using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorites : MonoBehaviour
{
    public int detectRadius;
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position+ Vector3.down,detectRadius);
        foreach(var item in colliders){
            if(item.tag == "Plain"){
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
            if(item.tag == "Enemy"){
                item.GetComponent<Enemy>().PushBack(1f);
            }
            if(item.tag == "Element"){
                Destroy(item.gameObject);
            }

        }
        if(transform.position.y <-20){
            Destroy(this.gameObject);
        }
    }
}
