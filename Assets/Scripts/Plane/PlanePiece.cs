using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePiece : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y <-20){
            Destroy(this.gameObject);
        }
    }
}
