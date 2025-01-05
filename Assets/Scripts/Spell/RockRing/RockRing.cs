using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RockRing : MonoBehaviour
{
    public GameObject rock;
    public float ringRadius;
    public TMP_Text counter;

    public GameObject[] orbitingObjects;
    public float orbitRadius = 2f;
    public float orbitSpeed = 50f;
    public float orbitTime;
    private static float orbitTimer = 0;

    private float[] angles;

    void Start()
    {
        if(orbitTimer>0){
            orbitTimer+=orbitTime;
            Destroy(this.gameObject);
            return;
        }
        else{
            angles = new float[orbitingObjects.Length];
            float angleStep = 360f / orbitingObjects.Length;
            orbitTimer = orbitTime;
            for (int i = 0; i < orbitingObjects.Length; i++)
            {
                angles[i] = i * angleStep;
                orbitingObjects[i]  = Instantiate(rock);
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < orbitingObjects.Length; i++)
        {
            angles[i] += orbitSpeed * Time.deltaTime;
            angles[i] %= 360f;

            float radians = angles[i] * Mathf.Deg2Rad;

            Vector3 newPosition = new Vector3(
                transform.position.x + orbitRadius * Mathf.Cos(radians),
                transform.position.y,
                transform.position.z + orbitRadius * Mathf.Sin(radians)
            );

            orbitingObjects[i].transform.position = newPosition;
        }
        orbitTimer-=Time.deltaTime;
        counter.text = ((int)orbitTimer).ToString();
        counter.transform.eulerAngles = new Vector3(0,0,0);
        if(orbitTimer<0){
            foreach(var item in orbitingObjects){
                Destroy(item);
            }
            Destroy(this.gameObject);
        }
    }
}
