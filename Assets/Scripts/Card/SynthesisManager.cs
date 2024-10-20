using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SynthesisManager : MonoBehaviour
{
    
    public List<ElementCards> cards;
    public SelectEventSO OnClickEvent;
    public SynthesisMethodSO synthesisMethodSO;
    public CardDeck cardDeck;
    public SpellName spellName;

    private void OnEnable()
    {
        OnClickEvent.OnEventRise += RestoreCard;
    }
    private void OnDisable()
    {
        OnClickEvent.OnEventRise -= RestoreCard;
    }
    public void RestoreCard(ElementCards card){
        cards.Add(card);
        CheckSynthesis();
    }
    
    private void CheckSynthesis(){
        if(cards.Count >= 2){
            synthesisMethodSO.element1 = cards[0].elementType;
            synthesisMethodSO.element2 = cards[1].elementType;
            spellName = synthesisMethodSO.SynthesisMethod();
            
            cardDeck.RemoveCard(cards[0].currentSlot);
            cardDeck.RemoveCard(cards[1].currentSlot);

            Debug.Log(spellName);
            cards.Clear();
        }
    }

}
