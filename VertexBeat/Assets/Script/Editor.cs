using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
    public Slider progressBar;
    public float runningTime;
    public bool isRunning;
    private void Awake(){
        if(isRunning){
            progressBar.maxValue = runningTime;
            StartCoroutine(ProgressBar());
        }
    }
    IEnumerator ProgressBar(){
    progressBar.value += Time.deltaTime * 1f;
    yield return new WaitForSeconds(1f);
    }
}
