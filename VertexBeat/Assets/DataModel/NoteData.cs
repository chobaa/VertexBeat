using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteData : MonoBehaviour
{
    public static NoteData instance = null;

    public List<Tuple<int, float>> noteDataList;   // 노트 정보 <key, time>

    public bool isNextNote = false;

    public double beatPerSample; // 노래의 beatSample 단위 기록
    public int[] target_cnt; // Sample 단위 수정

    void Awake()
    {
        // 싱글톤 디자인
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
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
