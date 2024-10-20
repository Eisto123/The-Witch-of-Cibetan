using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;


public class Cards : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,
IPointerEnterHandler, IPointerExitHandler,IPointerUpHandler,IPointerDownHandler
{
    public bool isSpell;
    public bool isDragging;
    public UnityEvent<Cards> BeginDragEvent;
    public UnityEvent<Cards> EndDragEvent;
    public UnityEvent<Cards> BeginClickEvent;
    public RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
    }
    private void Update()
    {
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEvent.Invoke(this);

        isDragging = true;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(GetComponent<RectTransform>().anchoredPosition.y > 150){
            Debug.Log("cast spell!");

        }
        //only spell card can drag
        transform.position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        EndDragEvent.Invoke(this);
        isDragging = false;
    }

    //Click:Select and synthesis spell
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {

    }

    //Hover: play some animation
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
