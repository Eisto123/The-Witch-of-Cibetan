using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elements : MonoBehaviour
{
    public ElementType type;

    public UnityEvent<ElementType> PickUpEvent;
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
        PickUpEvent.Invoke(type);
        //Add card, distroy this
        Destroy(this.gameObject);
    }
}
