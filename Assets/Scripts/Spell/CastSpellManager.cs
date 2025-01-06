using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CastSpellManager : MonoBehaviour
{
    private GameObject Player;
    public CastMethod castMethod;
    public CardDeck cardDeck;

    public UnityEvent OnCastEnd;


    [Header("SpellCanvas")]
    public Canvas skillShotCanvas;
    public Image skillShotImage; //For changing size
    public Canvas rangeCanvas;
    public Canvas rangeIndicator;
    public Image rangeImage;
    public float maxRangeDistance = 7f;
    public Canvas selfIndicator;


    public CastSpellEventSO spellEventSO;
    private Vector3 mousePosition;
    private RaycastHit hit;
    private Ray ray;
    private Vector3 castOffset;
    private SpellCards currentCard;
    
    [Header("SpellPrefab")]
    public Wave wavePrefab;
    public Fireball fireballPrefab;
    public Storm stormPrefab;
    public LandForming landFormingPrefab;
    public Meteorites meteoritesPrefab;
    public RockRing rockRingPrefab;
    public Earthquake earthquakePrefab;

    

    private void Awake()
    {
        Player= transform.parent.gameObject;
    }
    private void Start()
    {
        skillShotCanvas.enabled = false;
        rangeCanvas.enabled = false;
        rangeIndicator.enabled = false;
        selfIndicator.enabled = false;

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
        OnRangeShot();
        OnSelfCast();
    }

    public void OnSkillShot(){
        if(skillShotCanvas.enabled){
            if(Physics.Raycast(ray,out hit, Mathf.Infinity)){
                mousePosition = new Vector3(hit.point.x, 0,hit.point.z);
                //castOffset = mousePosition.normalized;
            }
            Quaternion ssCanvas = Quaternion.LookRotation(mousePosition - Player.transform.position);
            ssCanvas.eulerAngles = new Vector3(0, ssCanvas.eulerAngles.y,ssCanvas.eulerAngles.z);
            skillShotCanvas.transform.rotation = ssCanvas;
            if(Input.GetMouseButtonUp(0)){
                
                if(currentCard.spellName == SpellName.Wave){
                    Instantiate(wavePrefab,Player.transform.position + castOffset,ssCanvas);
                    
                }
                else if(currentCard.spellName == SpellName.Fireball){
                    Instantiate(fireballPrefab,Player.transform.position,ssCanvas);
                }
                OnCastEnd.Invoke();
                cardDeck.RemoveCard(currentCard.currentSlot);
                skillShotCanvas.enabled = false;
                
                
                
            }
        }
    }

    public void OnRangeShot(){
        if(rangeCanvas.enabled){
            LayerMask layerMask = LayerMask.GetMask("Plain");
            if(Physics.Raycast(ray, out hit, Mathf.Infinity,layerMask)){
                mousePosition = new Vector3(hit.point.x, 0,hit.point.z);
            }
            var hitPointDir = (mousePosition - transform.position).normalized;
            float distance = Vector3.Distance(hit.point,transform.position);
            distance = Mathf.Min(distance, maxRangeDistance);

            var newHitPos = transform.position + hitPointDir*distance;
            rangeCanvas.transform.position = newHitPos;
            if(Input.GetMouseButtonUp(0)){
                if(currentCard.spellName == SpellName.Storm){
                    Instantiate(stormPrefab,rangeCanvas.transform.position,Quaternion.identity);
                }
                if(currentCard.spellName == SpellName.Meteorites){
                    Instantiate(meteoritesPrefab,rangeCanvas.transform.position+Vector3.up*5,Quaternion.identity);
                    
                }
                OnCastEnd.Invoke();
                cardDeck.RemoveCard(currentCard.currentSlot);
                rangeCanvas.enabled = false;
                rangeIndicator.enabled = false;
            }
        }
    }

    private void OnSelfCast(){
        if(selfIndicator.enabled){
            if(Input.GetMouseButtonUp(0)){
                if(currentCard.spellName == SpellName.RockRing){
                    Instantiate(rockRingPrefab,Player.transform.position,Quaternion.identity,Player.transform);
                }
                if(currentCard.spellName == SpellName.Earthquake){
                    Instantiate(earthquakePrefab,Player.transform.position,Quaternion.identity,Player.transform);
                }
                OnCastEnd.Invoke();
                cardDeck.RemoveCard(currentCard.currentSlot);
                selfIndicator.enabled = false;
            }
            
        }
    }

    public void OnCastSpell(SpellCards spellcard){
        switch(spellcard.castMethod){
                case CastMethod.SkillShot:
                skillShotCanvas.enabled = true;
                currentCard = spellcard;
                return;

                case CastMethod.Range:
                rangeCanvas.enabled = true;
                rangeIndicator.enabled = true;
                currentCard = spellcard;
                return;

                case CastMethod.Self:
                selfIndicator.enabled = true;
                currentCard = spellcard;
                return;

            }
        //enable canvas
    }

}
