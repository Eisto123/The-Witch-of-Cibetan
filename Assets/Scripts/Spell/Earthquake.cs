using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Earthquake : MonoBehaviour
{
    public float earthquakeRadius;
    public float force;
    private CinemachineImpulseSource source;
    public AudioClip earthquakeClip;
    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }
    void Start()
    {
        ScreenShakeManager.instance.CameraShake(source);
        SFXManager.instance.PlayClip(earthquakeClip);
        Collider[] colliders = Physics.OverlapSphere(transform.position, earthquakeRadius); 
            foreach (Collider hit in colliders) {
                if(hit.tag == "Enemy"){
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy != null) { 
                    enemy.PushBack(force);
                }
            }
        }
        SFXManager.instance.FadeOutClip();

    }
}
