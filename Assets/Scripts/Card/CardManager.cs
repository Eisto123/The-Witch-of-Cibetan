using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CardManager : MonoBehaviour
{
    public ElementCardSO water;
    public ElementCardSO fire;
    public ElementCardSO earth;

    [Header("CardPrefab")]
    public GameObject elementCard;
    public GameObject spellCard;

    public CardDeck cardDeck;


    public List<SpellCardSO> spellCards;
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
        cardDeck.AddCard(elementCard);
    }

    public void OnPickUpEvent(ElementType elementType){
        switch(elementType){
            case ElementType.Water:
                elementCard.GetComponent<ElementCards>().Inicialize(water);
                cardDeck.AddCard(elementCard);
                return;
            case ElementType.Fire:
                elementCard.GetComponent<ElementCards>().Inicialize(fire);
                cardDeck.AddCard(elementCard);
                return;
            
            case ElementType.Earth:
                elementCard.GetComponent<ElementCards>().Inicialize(earth);
                cardDeck.AddCard(elementCard);
                return;
        }
    }

    public void AddSpellCard(SpellName spellName){
        
        if(spellCards != null){
            foreach(SpellCardSO item in spellCards){
                if(item.spellName == spellName){
                    spellCard.GetComponent<SpellCards>().Inicialize(item);
                    cardDeck.AddCard(spellCard);
                }
            }
        }
    }

}
