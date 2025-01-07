using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;

    void Update()
    {
        bulletLifeTime -= Time.deltaTime;
        transform.position += transform.forward*bulletSpeed*Time.deltaTime;
        if(bulletLifeTime<=0){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage();
        }
    }
}
