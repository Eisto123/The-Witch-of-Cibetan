using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementCards : Cards
{
    public ElementType elementType;
    public Text text;
    public Image image;
    
    public Vector2 clickOffset;

    private bool startSelecting = false;
    public bool isSelected = false;

    //Iniciate different element on the card
    public void Inicialize(ElementCardSO cardSO){
        
        elementType = cardSO.elementType;
        text.text = cardSO.elementName;
        image.sprite = cardSO.image;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        startSelecting = true;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if(!isSelected&&startSelecting&&!isDragging){
        rect.anchoredPosition += clickOffset;
        BeginClickEvent.Invoke(this);
        isSelected = true;
        startSelecting = false;
        }
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if(!isSelected){
            rect.anchoredPosition += hoverOffset;
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if(!isSelected){
            rect.anchoredPosition = Vector3.zero;
        }
    }

}
