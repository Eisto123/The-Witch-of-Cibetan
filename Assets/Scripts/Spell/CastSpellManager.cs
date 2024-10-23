using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;

public class CastSpellManager : MonoBehaviour
{
    private GameObject Player;
    public CastMethod castMethod;
    public Canvas skillShotCanvas;
    public Image skillShotImage;
    public CastSpellEventSO spellEventSO;
    private Vector3 mousePosition;
    private RaycastHit hit;
    private Ray ray;
    private Vector3 castOffset;
    private SpellCards currentCard;
    public Wave wavePrefab;

    public CardDeck cardDeck;
    

    private void Awake()
    {
        Player= transform.parent.gameObject;
    }
    private void Start()
    {
        skillShotCanvas.enabled = false;

    }

    private void OnEnable()
    {
        spellEventSO.OnEventRise += OnCastSpell;
    }
    private void OnDisable()
    {
        spellEventSO.OnEventRise -= OnCastSpell;
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        OnSkillShot();
    }

    public void OnSkillShot(){
        if(skillShotCanvas.enabled){
            if(Physics.Raycast(ray,out hit, Mathf.Infinity)){
                mousePosition = new Vector3(hit.point.x, 0,hit.point.z);
                castOffset = mousePosition.normalized;
            }
            Quaternion ssCanvas = Quaternion.LookRotation(mousePosition - Player.transform.position);
            ssCanvas.eulerAngles = new Vector3(0, ssCanvas.eulerAngles.y,ssCanvas.eulerAngles.z);
            skillShotCanvas.transform.rotation = ssCanvas;
            if(Input.GetMouseButtonUp(0)){
                
                if(currentCard.spellName == SpellName.Wave){
                    Instantiate(wavePrefab,Player.transform.position + castOffset,ssCanvas);
                    cardDeck.RemoveCard(currentCard.currentSlot);
                }
                skillShotCanvas.enabled = false;
                
            }
        }
    }
    public void OnCastSpell(SpellCards spellcard){
        switch(spellcard.castMethod){
                case CastMethod.SkillShot:
                skillShotCanvas.enabled = true;
                currentCard = spellcard;
                //perform actual skill and turn off.
                return;
            }
        //enable canvas
    }

}
