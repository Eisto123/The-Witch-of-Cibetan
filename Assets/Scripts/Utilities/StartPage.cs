using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
    public void LoadMainScene(){
        SceneManager.LoadScene("MainScene");
    }

    public void LoadTutorial(){
        SceneManager.LoadScene("Tutorial");
    }
}
