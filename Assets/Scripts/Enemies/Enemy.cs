using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    private Rigidbody rb;
    public GameplayManager gameplayManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.LookAt(player.transform);
        Vector3 direction = (player.transform.position - transform.position).normalized;
	  	rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        if(transform.position.y <-20){
            gameplayManager.enemyLeft--;
            gameplayManager.UpdateUI();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player"){
            gameplayManager.HP --;
            gameplayManager.UpdateUI();
        }
    }
}
