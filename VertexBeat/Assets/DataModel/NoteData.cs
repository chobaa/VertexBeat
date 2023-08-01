using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteData : MonoBehaviour
{
    public static NoteData instance = null;

     public List<Tuple<int, float>> noteDataList;   // 노트 정보 <key, time>

    void Awake()
    {
        // 싱글톤 디자인
        if(instance == null){
            instance = this;
        }
        else{
            if(instance != this)
                Destroy(this.gameObject);
        }

        noteDataList = new List<Tuple<int, float>>();
    }

    public List<Tuple<int, float>> getNoteDataList()
    {
        return noteDataList;
    }
    public void setNoteDataList(List<Tuple<int, float>> noteDataList)
    {
        this.noteDataList.AddRange(noteDataList);
    }
}
