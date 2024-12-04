using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32;

//using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    private Rigidbody rb;
    public GameplayManager gameplayManager;
    public bool isStun;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
    }

    private void FixedUpdate()
    {
        if(!isStun){
            transform.LookAt(player.transform);
            //Vector3 direction = (player.transform.position - transform.position).normalized;
	  	    //rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
            rb.AddForce(transform.forward*moveSpeed*Time.deltaTime);
        }
        
        if(transform.position.y <-20){
            gameplayManager.enemyLeft--;
            gameplayManager.UpdateUI();
            Destroy(this.gameObject);
        }
    }
    private IEnumerator OnStun(){
        yield return new WaitForSeconds(1f);
        isStun = false;
    }

    public void Stun(){
        isStun = true;
        StartCoroutine(OnStun());
    }
    public void PushBack(float power){
        isStun = true;
        rb.AddForce(-transform.forward * power,ForceMode.Impulse);
        StartCoroutine(OnStun());
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player"){
            gameplayManager.HP --;
            gameplayManager.UpdateUI();
        }
    }
}
