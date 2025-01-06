using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float fireSpeed;
    public float spellDistance;
    private Vector3 initialPos;
    //private Vector3 DetectPos;

    private CinemachineImpulseSource source;

    public float Force;
    public float detectRadius = 1;


    public bool explode = false;
    
    private void Awake()
    {
        initialPos = transform.position;
        //DetectPos = initialPos;
        source = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        transform.position += transform.forward*fireSpeed*Time.deltaTime;
        //DetectPos = transform.position;
        
        if(explode){
            ScreenShakeManager.instance.CameraShake(source);
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius); 
            foreach (Collider hit in colliders) {
                if(hit.tag == "Enemy"){
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null) { 
                    enemy.PushBack(Force);
                }
            }
            }
            Destroy(this.gameObject);
            
        }
        if((transform.position - initialPos).magnitude > spellDistance){
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy"){
            explode = true;
        }
    }
}
   
