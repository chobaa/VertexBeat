using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongData : MonoBehaviour
{
    public string title;                // 타이틀
    public string artist;               // 아티스트
    public double bpm;                  // BPM
    public List<NoteData> noteDataList;   // 노트 데이터 리스트
    public int lnType;                  // 롱노트 타입

    public int totalFigureCount;          // 총 도형의 갯수
    public int totalNoteCount;           // 총 Note 수
    public double totalPlayTime;         // 총 재생 시간

    void Awake()
    {
        title = "";
        artist = "";
        bpm = 0;
        noteDataList = new List<NoteData>();
        totalNoteCount = 0;
        totalPlayTime = 0;
        lnType = 0;
    }

    // get/set
    public string getTitle()
    {
        return title;
    }
    public void setTitle(string title)
    {
        this.title = title;
    }
    public string getArtist()
    {
        return artist;
    }
    public void setArtist(string artist)
    {
        this.artist = artist;
    }
    public List<NoteData> getNoteDataList()
    {
        return noteDataList;
    }
    public void setNoteDataList(List<NoteData> noteDataList)
    {
        this.noteDataList = noteDataList;
    }
    public double getBpm()
    {
        return bpm;
    }
    public void setBpm(double bpm)
    {
        this.bpm = bpm;
    }
    public int getTotalNoteCount()
    {
        return totalNoteCount;
    }
    public void setTotalNoteCount(int totalCount)
    {
        this.totalNoteCount = totalCount;
    }
    public int getLnType()
    {
        return lnType;
    }
    public void setLnType(int lnType)
    {
        this.lnType = lnType;
    }
    public double getTotalPlayTime()
    {
        return totalPlayTime;
    }
    public void setTotalPlayTime(double totalPlayTime)
    {
        this.totalPlayTime = totalPlayTime;
    }

    // add
    public void addNoteData(NoteData note)
    {
        noteDataList.Add(note);
    }

    // debug
    public void debug()
    {
        print("title = " + title);
        print("artist = " + artist);
        print("bpm = " + bpm);
        print("long note type = " + lnType);
        print("total figure Count = " + noteDataList.Count);
        print("total note Count = " + totalNoteCount);
        print("total play time = " + totalPlayTime);
    }
}
