using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class GameplayManager : MonoBehaviour
{
    
    public GameObject elementPrefab;
    public float generateElementTime;
    private float elementTimeCounter;

    public GameObject enemyPrefab;
    public float generateEnemyTime;
    public int enemyInstanciateRadius;
    public float enemyShootAngle;
    private float enemyTimeCounter;
    
    public float radius= 10f;
    public int HP = 3;
    public Text HPText;
    public int enemyLeft = 2;
    public Text EnemyText;
    public Text tutorialText;
    public GameObject bossPrefab;
    public PickUpEventSO pickUpEvent;
    public bool onBossStage;
    private BossState currentState;
    private GameObject boss;

    public GameObject Win;
    public GameObject Lose;

    private void Start()
    {
        Time.timeScale = 1;
        UpdateUI();
        enemyTimeCounter = generateEnemyTime;
        onBossStage = false;
    }

    private void Update()
    {
        Counter();
        if(HP <= 0||transform.position.y<-20){
            
            Lose.SetActive(true);
            Time.timeScale = 0;
        }
        if(currentState == BossState.Die){
            Win.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
    private void Counter()
    {
        enemyTimeCounter -= Time.deltaTime;
        elementTimeCounter -= Time.deltaTime;
        if (elementTimeCounter <= 0)
        {
            GenerateElement();
            elementTimeCounter = generateElementTime;
        }
        if(enemyTimeCounter<= 0&&onBossStage){
            //GenerateEnemy();
            enemyTimeCounter = generateEnemyTime;
        }
    }
    private void GenerateElement(){
        int randomResult = Random.Range(0,3);
        if(randomResult == 0){
            elementPrefab.GetComponent<Elements>().type = ElementType.Water;
        }
        else if(randomResult == 1){
            elementPrefab.GetComponent<Elements>().type = ElementType.Fire;
        }
        else{
            elementPrefab.GetComponent<Elements>().type = ElementType.Earth;
        }
        
        Vector3 InsPos = Random.insideUnitSphere * radius;
        InsPos = new Vector3 (InsPos.x, 0.5f, InsPos.z);
        Instantiate (elementPrefab,InsPos,Random.rotation);
    }

    private void GenerateEnemy(){
        var enemy = Instantiate (enemyPrefab);
        LaunchObjectToPlain(enemy);
    }

    public void LaunchObjectToPlain(GameObject gameObject){
        Vector3 endPos = Random.insideUnitSphere * radius;
        endPos = new Vector3 (endPos.x, 0.5f, endPos.z);
        Vector3 insPos = Random.onUnitSphere*enemyInstanciateRadius;
        insPos = new Vector3 (insPos.x, 0.5f, insPos.z);
        gameObject.transform.position = insPos;
        Rigidbody rb = gameObject.GetComponentInChildren<Rigidbody>();
        Vector3 velocity = CalculateVelocity(endPos, insPos, enemyShootAngle);
        rb.velocity = velocity;
    }
    private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float angle)
    {
        Vector3 direction = target - origin;
        direction.y = 0;
        float horizontalDistance = direction.magnitude;

        float angleRad = angle * Mathf.Deg2Rad;

        float initialVelocity = Mathf.Sqrt((horizontalDistance * Physics.gravity.magnitude) / (Mathf.Sin(2 * angleRad)));

        Vector3 velocity = direction.normalized * initialVelocity * Mathf.Cos(angleRad);
        velocity.y = initialVelocity * Mathf.Sin(angleRad);

        return velocity;
    }

    public void DecreaseEnemy(){
        if(!onBossStage){
            enemyLeft--;
            UpdateUI();
            if(enemyLeft<=0){
                onBossStage = true;
                GenerateBoss();
            }
        }
    }


    public void UpdateUI(){
        HPText.text = "HP: "+ HP;
        if(enemyLeft > 0){
            EnemyText.text = "Enemy Left: " + enemyLeft;
        }
        else{
            EnemyText.text = "BossStage: " + currentState;
        }
        if(currentState == BossState.Die){
            EnemyText.text = "WIN";
        }
        
    }
    private void GenerateBoss(){
        boss = Instantiate(bossPrefab);
        LaunchObjectToPlain(boss);
    }
    public void UpdateBossStage(BossState bossState){
        currentState = bossState;
        if(currentState != BossState.Die){
            LaunchObjectToPlain(boss);
            UpdateUI();
        }
    }

    public void PlayerTakeDamage(){
        HP--;
        UpdateUI();
    }

    public void RestartGame(){
        SceneManager.LoadScene("MainScene");
    }

}
