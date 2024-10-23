using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SpellCards : Cards
{
    public SpellName spellName;
    public Text text;
    public Image image;
    public CastMethod castMethod;
    public UnityEvent<SpellCards> CastSkillEvent;
    private bool isCasting;
    public int dragAmount = 200;

    public void Inicialize(SpellCardSO spellSO){
        
        spellName = spellSO.spellName;
        text.text = spellSO.spellName.ToString();
        castMethod = spellSO.castMethod;

    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        if(GetComponent<RectTransform>().anchoredPosition.y > 200&&!isCasting){
            CastSkillEvent.Invoke(this);
            image.enabled = false;
            text.enabled = false;
            isCasting = true;
        }
        transform.position = eventData.position;
        //only spell card can drag
        
    }
}
