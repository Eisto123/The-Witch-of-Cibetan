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

    private void Start()
    {

    }
    private void GenerateSlot(){
        for(int i = 0; i<cards.Count;i++){
            if(transform.childCount<=i){
                Instantiate(slotPrefab,transform);
            }
        }
        
        float totalWidth = cards.Count*cardWidth;
        int j = 0;
        foreach(Transform slot in transform){
            if(j<transform.childCount){
                float xPos = 0 - totalWidth/2 + (j*cardWidth) + cardWidth/2;
                rect = slot.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(xPos,0,0);
                cards[j].transform.SetParent(slot.transform);
                cards[j].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                j++;
            }

        }
        
        
        // for(int i = 0; i<cards.Count;i++){
        //     float xPos = 0 - totalWidth/2 + (i*cardWidth) + cardWidth/2;
        //     GameObject Slot = Instantiate(slotPrefab,transform);
        //     rect = Slot.GetComponent<RectTransform>();
        //     rect.anchoredPosition = new Vector3(xPos,0,0);
        //     cards[i].transform.SetParent(Slot.transform);
        //     cards[i].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        // }
    }

    public void AddCard(GameObject card){
        if(cards.Count < maxCard)
        cards.Add(Instantiate(card));
        GenerateSlot();
        //InsertCards();
    }

    private void InsertCards(){
        int index = 0;
        if (cards != null){
            foreach (Transform child in transform)
            {
                if(index<cards.Count){
                Instantiate(cards[index],child.transform);
                index++;
                break;
                }
                
            }
        }
    }

}
