using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Meteorites : MonoBehaviour
{
    public int detectRadius;
    public int detectOffset;
    private CinemachineImpulseSource source;
    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position+ Vector3.down*detectOffset,detectRadius);
        foreach(var item in colliders){
            if(item.tag == "Plain"){
                item.GetComponent<Rigidbody>().isKinematic = false;
                ScreenShakeManager.instance.CameraShake(source);
            }
            if(item.tag == "Enemy"){
                item.GetComponent<Enemy>().PushBack(1f);
            }
            if(item.tag == "Element"){
                Destroy(item.gameObject);
            }

        }
        if(transform.position.y <-2){
            Destroy(this.gameObject);
        }
    }
}
