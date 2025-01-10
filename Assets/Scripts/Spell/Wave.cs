using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float waveSpeed;
    public float spellDistance;
    public Rigidbody rb;
    private Vector3 initialPos;
    public AudioClip waveClip;
    
    private void Awake()
    {
        initialPos = transform.position;
        SFXManager.instance.PlayClip(waveClip);
    }

    private void Update()
    {
        
        //rb.AddRelativeForce(Vector3.forward*waveSpeed);
        if((transform.position - initialPos).magnitude > spellDistance){
            Destroy(this.gameObject);
            SFXManager.instance.FadeOutClip();
        }
    }
    private void FixedUpdate()
    {
        transform.position += transform.forward*waveSpeed*Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy>().Stun();
        }
    }

}
