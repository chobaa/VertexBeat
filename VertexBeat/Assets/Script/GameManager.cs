using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool data_load = false; // Data_Load

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            if(instance != this)
                Destroy(this.gameObject);
        }
    }
}
