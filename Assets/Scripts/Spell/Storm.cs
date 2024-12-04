using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    [Header("Impact")]
    [Range(0,1000)]
    public float Force;
    public float Radius;
    public float lift;

    private void Start()
    {
        Vector3 explosionPos = transform.position; 
         Collider[] colliders = Physics.OverlapSphere(explosionPos, Radius); 
         
         foreach (Collider hit in colliders) {
            if(hit.tag == "Enemy"){
                Enemy enemy = hit.GetComponent<Enemy>();
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null&&enemy != null) { 
                    enemy.Stun();
                    rb.AddExplosionForce(Force, explosionPos, Radius,lift); 
                }
            }
            
        }
        Destroy(this.gameObject);
    }

}
