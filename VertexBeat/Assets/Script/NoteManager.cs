using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    bool request = true;
    string songInfo = "test";
    DataManager theDataManager;
    public List<int> noteData = new List<int>(); // 노트가 나오는 순서
    // Start is called before the first frame update
    void Start()
    {
        theDataManager = FindObjectOfType<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(request){
            theDataManager.SongDataLoad(songInfo);
            request = false;
        }
    }
}
