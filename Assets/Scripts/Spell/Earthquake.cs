using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    public float earthquakeRadius;
    public float force;
    void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, earthquakeRadius); 
            foreach (Collider hit in colliders) {
                if(hit.tag == "Enemy"){
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null) { 
                    enemy.PushBack(force);
                }
            }
        }
    }
}
