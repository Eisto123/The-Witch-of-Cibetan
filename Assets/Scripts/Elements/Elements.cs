using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements : MonoBehaviour
{
    public ElementType type;

    public Material waterMaterial;
    public Material fireMaterial;

    private Renderer rd;

    private void Awake()
    {
        rd = GetComponent<Renderer>();
        switch(type){
            case ElementType.Water:
                rd.material = waterMaterial;
                return;
            case ElementType.Fire:
                rd.material = fireMaterial;
                return;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add card, distroy this
        Debug.Log("hit");
        Destroy(this.gameObject);
    }
}
