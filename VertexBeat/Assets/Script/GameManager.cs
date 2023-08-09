using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isPlaying = false;
    public bool data_load = false; // Data_Load
    public bool sync_load = false; // Sync_Load

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
        isPlaying = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SceneManager.GetActiveScene().name == "Lobby") SceneManager.LoadScene(1);
            else SceneManager.LoadScene(0);
        }
    }
}
