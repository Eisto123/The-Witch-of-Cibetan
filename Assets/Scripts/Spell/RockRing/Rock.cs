using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Enemy"){
            //other.GetComponent<Enemy>().Stun();
            other.GetComponent<Enemy>().PushBack(0.5f);
        }
    }
}
