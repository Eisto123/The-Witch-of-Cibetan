//using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private TutorialStage tutorialStage;
    public Text tutorialText;
    public GameObject elementPrefab;
    public PickUpEventSO pickUpEventSO;
    private int collectingStage = 0;
    public float radius;
    public Vector3 dummyPos;

    public GameObject dummyPrefab;
    private GameObject dummy;
    public GameObject Win;

    
    private IEnumerator CameraZoom(float startFOV,float endFOV,float zoomTime){
        float timePassed = 0;
        while(timePassed<zoomTime){
            float t = timePassed/zoomTime;
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(startFOV,endFOV,t);
            timePassed+=Time.deltaTime;
            yield return null;
        }
        
        
    }
    private void OnEnable()
    {
        pickUpEventSO.OnEventRise += OnPickUp;
    }

    

    private void OnDisable()
    {
        pickUpEventSO.OnEventRise -= OnPickUp;
    }
    void Start()
    {
        tutorialText.gameObject.SetActive(false);
        StartCoroutine(CameraZoom(20,60,1f));
    }

    // Update is called once per frame
    void Update()
    {
        if(virtualCamera.m_Lens.FieldOfView >= 55&&tutorialStage == TutorialStage.Starting){
            tutorialText.gameObject.SetActive(true);
            tutorialStage = TutorialStage.Moving;
            
        }
        switch(tutorialStage){
            case TutorialStage.Starting:
                break;
            case TutorialStage.Moving:
                OnMovingTutotial();
                break;
            case TutorialStage.Collecting:
                OnCollectingTutorial();
                break;
            case TutorialStage.Combining:
                OnCombiningTutorial();
                break;
            case TutorialStage.Casting:
                OnCastingTutorial();
                break;
            case TutorialStage.Dummy:
                OnDummyTutorial();
                break;
            case TutorialStage.End:
                OnEndTutorial();
                break;
        }
    }

    private void OnMovingTutotial()
    {
        
        tutorialText.text = "W  A  S  D";
        if(Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.S)||Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D)){
            tutorialStage = TutorialStage.Collecting;
            GenerateElement();
        }
        
    }

    private void OnCollectingTutorial()
    {
        if(collectingStage == 0){
            tutorialText.text = "Pick up the cube";
            
        }
        else if(collectingStage == 1){
            tutorialText.text = "Pick up another one";
        }
        else{
            tutorialStage = TutorialStage.Combining;
        }
        
    }

    private void GenerateElement(){
        if(collectingStage<2){
            Vector3 InsPos = Random.insideUnitSphere * radius;
            InsPos = new Vector3 (InsPos.x, 0.5f, InsPos.z);
            if(collectingStage == 0){
                elementPrefab.GetComponent<Elements>().type = ElementType.Water;
            }
            else{
                elementPrefab.GetComponent<Elements>().type = ElementType.Fire;
            }
            Instantiate(elementPrefab,InsPos,Random.rotation);
        }
    }

    private void OnPickUp(ElementType type)
    {
        collectingStage++;
        GenerateElement();
    }

    private void OnCombiningTutorial()
    {
        tutorialText.text = "Click two card to generate spell";
    }

    public void SynthesisComplete(){
        if(tutorialStage == TutorialStage.Combining){
            tutorialStage = TutorialStage.Casting;
        }
    }
    private void OnCastingTutorial()
    {
        tutorialText.text = "Drag the card to cast the spell";
    }
    public void CastComplete(){
        if(tutorialStage == TutorialStage.Casting){
            StartCoroutine(CastCompleteProcess());
        }
        if(tutorialStage == TutorialStage.Dummy){
            collectingStage = 1;
            GenerateElement();
            GenerateElement();
        }
        
        
    }
    private IEnumerator CastCompleteProcess(){
        tutorialText.text = "Amazing! You just cast a storm";
        yield return new WaitForSeconds(1f);
        collectingStage = 1;
        GenerateElement();
        GenerateElement();
        dummy = Instantiate(dummyPrefab,dummyPos,Quaternion.Euler(0f,-90f,0f));
        tutorialStage = TutorialStage.Dummy;
    }

    private void OnDummyTutorial()
    {
        tutorialText.text = "Try hit the Dummy off the platform";
        if(dummy.GetComponentInChildren<Enemy>().transform.position.y<0){
            tutorialStage = TutorialStage.End;
            StartCoroutine(EndProcess());
        }
    }

    private void OnEndTutorial()
    {
        
    }
    private IEnumerator EndProcess(){
        tutorialText.text = "You complete the tutorial";
        yield return new WaitForSeconds(2f);
        tutorialText.text = "Use your wisdom as weapon. Don't get caught by the Dark";
        yield return new WaitForSeconds(2f);
        Win.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadMainScene(){
        SceneManager.LoadScene("MainScene");
    }

    
}
