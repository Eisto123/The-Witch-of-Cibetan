using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletLifeTime -= Time.deltaTime;
        transform.position += transform.forward*bulletSpeed*Time.deltaTime;
        if(bulletLifeTime<=0){
            Destroy(this.gameObject);
        }
    }
}
