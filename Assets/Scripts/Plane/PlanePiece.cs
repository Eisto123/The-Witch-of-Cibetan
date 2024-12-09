using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePiece : MonoBehaviour
{
    public Rigidbody rb;

    void Update()
    {
        if(transform.position.y <-20){
            Destroy(this.gameObject);
        }
        if(!rb.isKinematic){
            StartCoroutine(DestroyProcess());
        }
    }
    private IEnumerator DestroyProcess(){
        
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
