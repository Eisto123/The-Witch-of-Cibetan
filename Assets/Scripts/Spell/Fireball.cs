using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireSpeed;
    public float spellDistance;
    private Vector3 initialPos;
    [Header("Impact")]
    [Range(0,1000)]
    public float Force = 100; // Netwons
    [Range(0.1f, 1)]
    public float Radius = 0.25f; // m
    public float detectRadius = 5;
    public float lift = 30;
    public bool explode = false;
    
    private void Awake()
    {
        initialPos = transform.position;
        
    }

    private void Update()
    {
        
        //rb.AddRelativeForce(Vector3.forward*waveSpeed);
        if((transform.position - initialPos).magnitude > spellDistance){
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.position += transform.forward*fireSpeed*Time.deltaTime;
        if(explode){ 
         Vector3 explosionPos = transform.position; 
         Collider[] colliders = Physics.OverlapSphere(explosionPos, detectRadius); 
         
         foreach (Collider hit in colliders) {
            Debug.Log(hit);
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) { 
               rb.AddExplosionForce(Force, explosionPos, detectRadius); 
            } 
        }
        Destroy(this.gameObject);
        } 
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy"){
            explode = true; 
            
        }
        
    }
}
   
