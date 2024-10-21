using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SpellCards : Cards
{
    public SpellName spellName;
    public Text text;
    public Image image;
    public int dragAmount = 200;

    public void Inicialize(SpellCardSO spellSO){
        
        spellName = spellSO.spellName;
        text.text = spellSO.spellName.ToString();

    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if(GetComponent<RectTransform>().anchoredPosition.y > 200){
            Debug.Log("cast spell!");

        }
        //only spell card can drag
        transform.position = eventData.position;
    }
}
