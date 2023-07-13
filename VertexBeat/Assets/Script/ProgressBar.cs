using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProgressBar : MonoBehaviour
{
    public Image loadingBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        loadingBar.fillAmount += 0.001f;
    }
}
