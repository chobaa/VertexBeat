using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongData : MonoBehaviour
{
    public static SongData instance = null;

    public string title;                // 타이틀
    public string artist;               // 아티스트
    public double bpm;                  // BPM
    public double signature;            // Signature
    public double offset;                   // Offset
    public double firstNoteSample; // 첫 번째 note의 시간 기록

    public int totalFigureCount;          // 총 도형의 갯수
    public int totalNoteCount;           // 총 Note 수
    public double totalPlayTime;         // 총 재생 시간
    public double currentPlayTime;      // 현재 재생 시간

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

        title = "";
        artist = "";
        bpm = 0;
        totalNoteCount = 0;
        totalPlayTime = 0;
        currentPlayTime = 0;
        signature = 0;
        offset = 0;
    }

    //set
    public void setTitle(string title)
    {
        this.title = title;
    }
    public void setArtist(string artist)
    {
        this.artist = artist;
    }
    public void setBpm(double bpm)
    {
        this.bpm = bpm;
    }
    public void setTotalNoteCount(int totalCount)
    {
        this.totalNoteCount = totalCount;
    }
    public void setTotalPlayTime(double totalPlayTime)
    {
        this.totalPlayTime = totalPlayTime;
    }

    public void setFirstNoteSample(double firstNoteSample)
    {
        this.firstNoteSample = firstNoteSample;
    }

    // debug
    public void debug()
    {
        print("title = " + title);
        print("artist = " + artist);
        print("bpm = " + bpm);
        print("total note Count = " + totalNoteCount);
        print("total play time = " + totalPlayTime);
    }
}
