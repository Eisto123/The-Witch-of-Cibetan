using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Storm : MonoBehaviour
{
    [Header("Impact")]
    [Range(0,1000)]
    public float Force;
    public float Radius;
    public float lift;
    private Vector3 explosionPos;
    public GameObject strike;
    private CinemachineImpulseSource source;
    public AudioClip stormClip;

    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }
    private void Start()
    {
        explosionPos = transform.position;
        StartCoroutine(DestroyProcess());
        SFXManager.instance.PlayClip(stormClip);
        
    }
    private IEnumerator DestroyProcess(){
        yield return new WaitForSeconds(0.5f);
        strike.SetActive(false);
        
        Collider[] colliders = Physics.OverlapSphere(explosionPos, Radius); 
         foreach (Collider hit in colliders) {
            if(hit.tag == "Enemy"){
                Enemy enemy = hit.GetComponent<Enemy>();
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null&&enemy != null) { 
                    enemy.Stun();
                    rb.AddExplosionForce(Force, explosionPos, Radius,lift); 
                }
            }
            
        }
        ScreenShakeManager.instance.CameraShake(source);
        SFXManager.instance.FadeOutClip();
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        
    }


}
