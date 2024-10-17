using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCards : Cards
{
    public ElementType elementType;
    public Text text;
    public Image image;


    public void Inicialize(ElementCardSO cardSO){
        
        elementType = cardSO.elementType;
        text.text = cardSO.elementName;
        Debug.Log(elementType);
        Debug.Log(text.text);



    }
    
}
