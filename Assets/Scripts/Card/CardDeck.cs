using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public GameObject slotPrefab;

    public CastSpellEventSO castSpellEventSO;
    private RectTransform rect;
    public int cardWidth = 250;
    public int maxCard = 7;
    [SerializeField]private List<GameObject> cards;
    [SerializeField]private List<GameObject> Slots;
    public float angleBetweenCards;
    public float radius;
    private Vector3 centerPoint;
    public int centerOffset = 21;
    private bool isCastingSpell;
    private Cards currentCard;
    public float cardMoveSpeed;
    public float lerpDuration = 3f;

    private void Start()
    {
        centerPoint = Vector3.up * -centerOffset;
    }

    private void OnEnable()
    {
        castSpellEventSO.OnEventRise += OnCastSpell;
    }

    private void OnDisable()
    {
        castSpellEventSO.OnEventRise -= OnCastSpell;
    }

    private void Update()
    {
    }
    private void OnCastSpell(SpellCards card)
    {
        isCastingSpell = true;
        currentCard = card;
        for(int i = 0; i< cards.Count; i++){
            if(i != currentCard.currentSlot){
                StartCoroutine(LerpPos(cards[i],cards[i].GetComponent<RectTransform>().anchoredPosition,Vector3.up*-centerOffset/4));
            }
        }
    }

    IEnumerator LerpPos(GameObject card,Vector3 start, Vector3 end){
        float timePassed = 0;
        while(timePassed< lerpDuration){
            float t = timePassed/lerpDuration;
            if(card!= null){
                card.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(start, end, t*cardMoveSpeed);
            }
            timePassed+=Time.deltaTime;
            yield return null;
        }
        if(card!= null){
        card.GetComponent<RectTransform>().anchoredPosition = end;}
    }

    public void ResetPos(){
        foreach (var item in cards){
            float posY = Mathf.Lerp(item.GetComponent<RectTransform>().anchoredPosition.y,0, Time.deltaTime*cardMoveSpeed);
            item.GetComponent<RectTransform>().anchoredPosition = new Vector3 (0,posY,0);
            if(item!=null){
                StartCoroutine(LerpPos(item,item.GetComponent<RectTransform>().anchoredPosition,Vector3.zero));
            }
        }
        isCastingSpell = false;
    }


    private void GenerateSlot(){
        for(int i = 0; i<cards.Count;i++){
            
            if(Slots.Count<=i){

                Slots.Add(Instantiate(slotPrefab,transform));
            }
        }
        
        float angle = (cards.Count - 1)*angleBetweenCards/2;
        
        for(int index = 0; index<Slots.Count; index++){
            rect = Slots[index].GetComponent<RectTransform>();
            rect.anchoredPosition = FindCurvePosition(angle - index*angleBetweenCards);
            rect.rotation = Quaternion.Euler(0,0,angle - index*angleBetweenCards);
            cards[index].transform.SetParent(Slots[index].transform);
            if(!isCastingSpell){
                if(!cards[index].TryGetComponent<SpellCards>(out SpellCards spellCards)){
                    if(!cards[index].GetComponent<ElementCards>().isSelected){
                        cards[index].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    }
                }
                else{
                cards[index].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                }
            }
            else{
                if(index == cards.Count - 1){
                    cards[index].GetComponent<RectTransform>().anchoredPosition = Vector3.up*-centerOffset/4;
                }
            }
            cards[index].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0,0,0);
            cards[index].GetComponent<Cards>().currentSlot = index;
            
        }
    }

    private Vector3 FindCurvePosition(float angle){
        return new Vector3(
            centerPoint.x - Mathf.Sin(Mathf.Deg2Rad*angle)*radius, 
            centerPoint.y + Mathf.Cos(Mathf.Deg2Rad*angle)*radius,
            0
        );
    }

    public void AddCard(GameObject card){
        if(cards.Count < maxCard)
        cards.Add(Instantiate(card));
        GenerateSlot();
    }

    public void RemoveCard(int slotIndex){
        Destroy(Slots[slotIndex]);
        Slots.RemoveAt(slotIndex);
        cards.RemoveAt(slotIndex);
        GenerateSlot();
    }

}
