using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy>().PushBack(2f);
        }
    }
}
