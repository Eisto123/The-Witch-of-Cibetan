using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    private BossState bossState;
    private float dashTimer;
    public float dashWaitTime;
    public float dashForce;
    public int bossHealth = 2;
    public float detectRadius = 1;
    private bool onPlain;
    public int slayRange;
    public GameObject slayindicator;
    private Animator animator;
    public GameObject bullet;
    public float bulletDistance;
    public int bulletAmount;
    public float rotateAngle;
    
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

    // Update is called once per frame
    void Update()
    {
        gameplayManager.bossHP = bossHealth;
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
        }

        if(transform.position.y <-10){
            bossHealth--;
            if(bossHealth == 0){
                Destroy(this.gameObject);
                return;
            }
            rb.velocity = Vector3.zero;
            gameplayManager.LaunchBossBack(this.gameObject);
            bossState = bossState + 1;
        }
        
        
    }
    

    private void OnDash()
    {
        if(!isStun&&onPlain){
            transform.LookAt(player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            if(dashTimer<=0){
                rb.AddForce(transform.forward*dashForce,ForceMode.Impulse);
                dashTimer = dashWaitTime;
            }
            dashTimer -= Time.deltaTime;
            
        }
    }
    private void OnSlay()
    {
        if(!isStun&&onPlain){
            transform.LookAt(player.transform);
            rb.AddForce(transform.forward*moveSpeed);
            if((player.transform.position - transform.position).magnitude<= slayRange){
            StartCoroutine(SlayProcess());
        }
        }
        

        
    }
    private IEnumerator SlayProcess(){
        slayindicator.SetActive(true);
        isStun = true;
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Slay");
        yield return new WaitForSeconds(1f);
        slayindicator.SetActive(false);
        isStun = false;

    }


    private void OnShoot()
    {
        if(!isStun&&onPlain){
            transform.LookAt(player.transform);
            rb.AddForce(transform.forward*moveSpeed);
            if((player.transform.position - transform.position).magnitude<= slayRange){
                StartCoroutine(shootProcess());
        }
        }
    }

    private IEnumerator shootProcess(){
        isStun = true;
        Instantiate(bullet,transform);
            yield return new WaitForSeconds(3f);
        bullet.SetActive(false);
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
