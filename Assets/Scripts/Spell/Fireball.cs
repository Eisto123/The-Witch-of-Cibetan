using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireSpeed;
    public float spellDistance;
    private Vector3 initialPos;
    private Vector3 DetectPos;


    [Header("Impact")]
    [Range(0,1000)]
    public float Force; // Netwons
    [Range(0.1f, 1)]
    public float Radius = 0.25f; // m
    public float detectRadius = 5;


    public bool explode = false;
    
    private void Awake()
    {
        initialPos = transform.position;
        DetectPos = initialPos;
    }

    private void Update()
    {
        transform.position += transform.forward*fireSpeed*Time.deltaTime;
        //DetectPos = transform.position +transform.forward;
        
        if(explode){
            
            Collider[] colliders = Physics.OverlapSphere(DetectPos, detectRadius); 
            foreach (Collider hit in colliders) {
                if(hit.tag == "Enemy"){
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null) { 
                    enemy.PushBack(Force);
                }
            }
            
        }
            Destroy(this.gameObject);
            
        }
        if((transform.position - initialPos).magnitude > spellDistance){
            Destroy(this.gameObject);
        }
    }
    // private void FixedUpdate()
    // {
        
    //     if(explode){ 
    //      Vector3 explosionPos = transform.position; 
    //      Collider[] colliders = Physics.OverlapSphere(explosionPos, detectRadius); 
         
    //      foreach (Collider hit in colliders) {
    //         Debug.Log(hit);
    //         Rigidbody rb = hit.GetComponent<Rigidbody>();
    //         if (rb != null) { 
    //            rb.AddExplosionForce(Force, explosionPos, detectRadius); 
    //         } 
    //     }
    //     Destroy(this.gameObject);
    //     } 
    // }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy"){
            explode = true;
            
        }
    }
}
   
