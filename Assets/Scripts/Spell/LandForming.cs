using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandForming : MonoBehaviour
{
    public GameObject piece;
    public Transform plainParent;
    public float formingRadius = 1;

    private void Start()
    {
        plainParent = GameObject.Find("PlainGenerator").transform;
        PlacePrefab();
    }
    private void PlacePrefab()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, formingRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Plain")
            {
                Destroy(collider.gameObject); 
            }
        }
        
        var cubeLength = piece.transform.localScale.x;
        int count = ((int)(formingRadius/cubeLength))*2;
        Debug.Log(count);
        Debug.Log(cubeLength);
        for(int i = 0; i < count; i++){
            for(int j = 0; j< count; j++){
                if(new Vector2(-formingRadius+cubeLength*j,formingRadius-cubeLength*i).magnitude<formingRadius){
                    Debug.Log("called");
                    Instantiate(piece,new Vector3(transform.position.x-formingRadius+cubeLength*j,0,transform.position.z+formingRadius-cubeLength*i),Quaternion.identity,plainParent);
                }
                
            }
        }
    }
}
