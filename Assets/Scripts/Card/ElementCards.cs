using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCards : Cards
{
    public ElementType elementType;
    public Text text;
    public Image image;

    //Iniciate different element on the card
    public void Inicialize(ElementCardSO cardSO){
        
        elementType = cardSO.elementType;
        text.text = cardSO.elementName;



    }
    
}
