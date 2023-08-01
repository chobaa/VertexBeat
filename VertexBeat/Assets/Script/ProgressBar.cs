using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    public Image loadingBar;
    [SerializeField] double playtime;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.data_load){
            playtime = SongData.instance.totalPlayTime;
            loadingBar.fillAmount = (float)(SongData.instance.currentPlayTime / (playtime * 44100));
        }
    }
}
