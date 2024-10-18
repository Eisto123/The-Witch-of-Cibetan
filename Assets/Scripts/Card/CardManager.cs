using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardManager : MonoBehaviour
{
    public ElementCardSO water;
    public ElementCardSO fire;
    public GameObject elementCard;
    public GameObject cardDeck;

    public PickUpEventSO pickUpEvent;
    
    private void OnEnable()
    {
        pickUpEvent.OnEventRise += OnPickUpEvent;
    }
    private void OnDisable()
    {
        pickUpEvent.OnEventRise -= OnPickUpEvent;
    }

    [ContextMenu("TestGenerateCard")]
    public void GenerateCard(){
        cardDeck.GetComponent<CardDeck>().AddCard(elementCard);
    }
    public void OnPickUpEvent(ElementType elementType){
        switch(elementType){
            case ElementType.Water:
                elementCard.GetComponent<ElementCards>().Inicialize(water);
                cardDeck.GetComponent<CardDeck>().AddCard(elementCard);
                return;
            case ElementType.Fire:
                elementCard.GetComponent<ElementCards>().Inicialize(fire);
                cardDeck.GetComponent<CardDeck>().AddCard(elementCard);
                return;


        }
    }

}
