using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager instance;
    [SerializeField] private float globalShakeForce = 0.5f; 
    private void Awake()
    {
        if(instance == null){
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource){
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }
}
