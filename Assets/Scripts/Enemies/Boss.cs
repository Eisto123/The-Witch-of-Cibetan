using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Boss : Enemy
{
    private BossState bossState;
    private float dashTimer;
    public float dashWaitTime;
    public float dashForce;
    private bool onPlain;
    public int slayRange;
    public GameObject slayindicator;
    private Animator animator;
    public GameObject bullet;
    private bool isShooting = false;
    public float rotationSpeed = 5f;
    public UnityEvent<BossState> onStageChange;
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
        animator = GetComponent<Animator>();
        dashTimer = dashWaitTime;
        bossState = BossState.Dashing;
        
    }

    
    void FixedUpdate()
    {
        
        switch(bossState){
            case BossState.Dashing:
                OnDash();
                break;
            case BossState.Slaying:
                OnSlay();
                break;
            case BossState.Shooting:
                OnShoot();
                break;
            case BossState.Die:
                Destroy(this.gameObject);
                break;
        }

        if(transform.position.y <-20){
            rb.velocity = Vector3.zero;
            bossState = bossState + 1;
            gameplayManager.UpdateBossStage(bossState);
        }
        
        
    }

    private void LookAtPlayer(){
        if (player == null || rb == null) return;

            Vector3 directionToTarget = player.transform.position-new Vector3(0,1,0) - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            Quaternion smoothRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
    }
    

    private void OnDash()
    {
        if(!isStun&&onPlain){
            LookAtPlayer();
            
            if(dashTimer<=0){
                var target = new Vector3(player.transform.position.x,0,player.transform.position.z);
                StartCoroutine(DashPos(this.gameObject,transform.position,target));
                dashTimer = dashWaitTime;
            }
            dashTimer -= Time.deltaTime;
            
        }
    }
    IEnumerator DashPos(GameObject boss,Vector3 start, Vector3 end){
        if(!isStun){
            float timePassed = 0;
            float lerpDuration = 1.5f;
            while(timePassed< lerpDuration){
                float t = timePassed/lerpDuration;
                if(boss!= null){
                    boss.transform.position = Vector3.Lerp(start, end, t*dashForce);
                }
                if(isStun){
                    break;
                }
                timePassed+=Time.deltaTime;
                yield return null;
        }
        }
        
        
    }
    private void OnSlay()
    {
        if(!isStun&&onPlain){
            LookAtPlayer();
            rb.AddForce(transform.forward*moveSpeed);
            if((player.transform.position - transform.position).magnitude<= slayRange){
            StartCoroutine(SlayProcess());
        }
        }
        
    }
    private IEnumerator SlayProcess(){
        slayindicator.SetActive(true);
        isStun = true;
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Slay");
        yield return new WaitForSeconds(1f);
        slayindicator.SetActive(false);
        isStun = false;
        Vector3 directionToTarget = player.transform.position-new Vector3(0,1,0) - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        rb.MoveRotation(targetRotation);

    }


    private void OnShoot()
    {
        rotationSpeed = 30f;
        if(!isStun&&onPlain){
            LookAtPlayer();

            if(isShooting){
                StartCoroutine(shootProcess());
            }
            StartCoroutine(WaitRotation());
            
        }
    }

    private IEnumerator WaitRotation(){
        yield return new WaitForSeconds(1f);
        isShooting = true;
    }

    private IEnumerator shootProcess(){
        isStun = true;
        Instantiate(bullet,transform.position,transform.rotation);
        yield return new WaitForSeconds(0.6f);
        isStun = false;
        
    }
    
    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "Plain"){
            onPlain = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Plain"){
            onPlain = false;
        }
    }
}
