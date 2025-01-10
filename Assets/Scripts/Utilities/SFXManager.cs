using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    
    public AudioSource audioSource;
    private void Awake()
    {
        if(instance == null){
            instance = this;
        }
    }
    public void PlayClip(AudioClip clip){
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void StopClip(){
        audioSource.Stop();
    }
    public void FadeOutClip(){
        StartCoroutine(FadeOut(2f));
    }
    private IEnumerator FadeOut(float fadeTime){
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop ();
        audioSource.volume = startVolume;
    }


}
