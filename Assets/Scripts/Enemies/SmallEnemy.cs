using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy
{
    // Start is called before the first frame update
    private void Start()
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
}
