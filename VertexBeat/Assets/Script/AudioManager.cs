using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isStart;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        StartCoroutine(AudioPlay());
    }

    IEnumerator AudioPlay(){
        if(isStart){
            audioSource.Play();
            yield return null;
        }
    }
    //get/set
    public bool getStart()
    {
        return isStart;
    }
    public void setStartTrue()
    {
        this.isStart = true;
    }
    public void setStartFalse()
    {
        this.isStart = false;
    }
}
