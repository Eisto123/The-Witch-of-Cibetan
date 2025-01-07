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
    public Rigidbody rb;
    public GameplayManager gameplayManager;
    public bool isStun;


    
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
        rb.AddForce(-transform.forward * power + new Vector3(0,1f,0),ForceMode.Impulse);
        StartCoroutine(OnStun());
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage();
        }
    }
}
