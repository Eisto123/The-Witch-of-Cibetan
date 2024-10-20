using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementCards : Cards
{
    public ElementType elementType;
    public Text text;
    public Image image;
    public Vector2 clickOffset;
    public int currentSlot;

    public bool isSelected = false;

    //Iniciate different element on the card
    public void Inicialize(ElementCardSO cardSO){
        
        elementType = cardSO.elementType;
        text.text = cardSO.elementName;

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if(!isSelected){
        rect.anchoredPosition += clickOffset;
        BeginClickEvent.Invoke(this);
        isSelected = true;
        }
        

    }

}
