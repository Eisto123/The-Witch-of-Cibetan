using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    private RectTransform rect;
    public int cardWidth = 250;
    [SerializeField] private int maxCard = 7;
    public List<GameObject> cards;
    public List<GameObject> Slots;

    private void Start()
    {

    }
    private void GenerateSlot(){
        for(int i = 0; i<cards.Count;i++){
            Debug.Log("called");
            if(Slots.Count<=i){
                Debug.Log("insta slot");
                Slots.Add(Instantiate(slotPrefab,transform));
            }
        }
        
        float totalWidth = cards.Count*cardWidth;
        
        for(int index = 0; index<Slots.Count; index++){
            float xPos = 0 - totalWidth/2 + (index*cardWidth) + cardWidth/2;
                rect = Slots[index].GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(xPos,0,0);
                cards[index].transform.SetParent(Slots[index].transform);
                cards[index].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                cards[index].GetComponent<Cards>().currentSlot = index;
                if(cards[index].TryGetComponent<SpellCards>(out SpellCards spellCards)){
                }
                else{
                    if(!cards[index].GetComponent<ElementCards>().isSelected){
                    cards[index].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                }
                }
                
        }

        // int j = 0;
        // foreach(Transform slot in transform){
        //     if(j<transform.childCount){
        //         float xPos = 0 - totalWidth/2 + (j*cardWidth) + cardWidth/2;
        //         rect = slot.GetComponent<RectTransform>();
        //         rect.anchoredPosition = new Vector3(xPos,0,0);
        //         cards[j].transform.SetParent(slot.transform);
        //         cards[j].GetComponent<ElementCards>().currentSlot = j;
        //         if(!cards[j].GetComponent<ElementCards>().isSelected){
        //             cards[j].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        //         }
        //         j++;
        //     }

        // }
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
