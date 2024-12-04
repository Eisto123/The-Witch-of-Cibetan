using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour
{
    public GameObject Cube;
    public int ArenaRadius;
    //public List<Vector3> Blocks;

    // Start is called before the first frame update
    void Start()
    {
        var cubeLength = Cube.transform.localScale.x;
        int count = ((int)(ArenaRadius/cubeLength))*2;
        for(int i = 0; i < count; i++){
            for(int j = 0; j< count; j++){
                if(new Vector2(-ArenaRadius+cubeLength*j,ArenaRadius-cubeLength*i).magnitude<ArenaRadius){
                    Instantiate(Cube,new Vector3(-ArenaRadius+cubeLength*j,0,ArenaRadius-cubeLength*i),Quaternion.identity,this.transform);
                    //Blocks.Add(new Vector3(-ArenaRadius+cubeLength*j,0,ArenaRadius-cubeLength*i));
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
