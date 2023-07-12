using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteData : MonoBehaviour
{
    public List<Tuple<int, float>> noteDataList;   // 노트 정보 <key, time>

    void Awake()
    {
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
