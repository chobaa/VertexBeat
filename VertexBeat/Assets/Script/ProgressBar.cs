using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    public Image loadingBar;
    SongData theSongData;
    double playtime;
    // Start is called before the first frame update
    void Start()
    {
        theSongData = FindObjectOfType<SongData>();
        playtime = theSongData.getTotalPlayTime();
        Debug.Log(playtime);
    }

    // Update is called once per frame
    void Update()
    {
        loadingBar.fillAmount += 0.0001f;
    }
}
