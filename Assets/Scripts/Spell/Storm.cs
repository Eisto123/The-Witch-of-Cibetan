using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    [Header("Impact")]
    [Range(0,1000)]
    public float Force = 100; // Netwons
    public float Radius = 8; // m
    public float lift = 30;

    private void FixedUpdate()
    {
        Vector3 explosionPos = transform.position; 
         Collider[] colliders = Physics.OverlapSphere(explosionPos, Radius); 
         
         foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) { 
               rb.AddExplosionForce(Force, explosionPos, Radius,lift); 
            } 
        }
        Destroy(this.gameObject);
    }
    

}
