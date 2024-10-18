using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;


public class Cards : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool isDragging;
    public UnityEvent<Cards> BeginDragEvent;
    public UnityEvent<Cards> EndDragEvent;

    private void Update()
    {
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEvent.Invoke(this);

        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragEvent.Invoke(this);
        isDragging = false;
    }

}
