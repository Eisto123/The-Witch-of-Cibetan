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
            rb.AddForce(transform.forward*moveSpeed);
        }
        
        if(transform.position.y <-20){
            gameplayManager.DecreaseEnemy();
            Destroy(this.gameObject);
        }
    }
}
