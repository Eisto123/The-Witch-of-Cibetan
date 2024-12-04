using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Enemy"){
            other.GetComponent<Enemy>().Stun();
        }
    }
}
