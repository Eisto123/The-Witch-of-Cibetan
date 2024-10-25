using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject ElementPrefab;
    public float generateElementTime;
    public float TimeCounter;
    public float radius= 10f;
    public int HP = 3;
    public Text HPText;
    public int enemyLeft = 2;
    public Text EnemyText;

    public GameObject Win;
    public GameObject Lose;

    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        Counter();
        if(HP == 0||transform.position.y<-20){
            Lose.SetActive(true);
        }
        if(enemyLeft == 0){
            Win.SetActive(true);
        }
    }
    private void Counter()
    {
        TimeCounter -= Time.deltaTime;
        if (TimeCounter <= 0)
        {
            GenerateElement();
            TimeCounter = generateElementTime;
        }
    }
    private void GenerateElement(){
        
        if(Random.Range(0,2)==0){
            Debug.Log("water");
            ElementPrefab.GetComponent<Elements>().type = ElementType.Water;
        }
        else{
            Debug.Log("fire");
            ElementPrefab.GetComponent<Elements>().type = ElementType.Fire;
        }

        
        Vector3 InsPos = Random.insideUnitSphere * radius;
        InsPos = new Vector3 (InsPos.x, 0.5f, InsPos.z);
        Instantiate (ElementPrefab,InsPos,Random.rotation);
    }
    public void UpdateUI(){
        HPText.text = "HP: "+ HP;
        EnemyText.text = "Enemy Left: " + enemyLeft;
    }

    public void RestartGame(){
        SceneManager.LoadScene("MainScene");
    }

}
