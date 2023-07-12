using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteLength
{
    Short = 0,
    Long = 1,
}

public struct ShapeInfo
{
    public int length;
    public int type;

    public ShapeInfo(int length, int type)
    {
        this.length = length;
        this.type = type;
    }
}

public class Sheet
{
    // [Description]
    public string title;
    public string artist;

    // [Audio]
    public int bpm;
    public int offset;
    public int[] signature;

    // [ShapeInfo]
    public List<ShapeInfo> ShapeInfos = new List<ShapeInfo>();


    public AudioClip clip;
    public Sprite img;
    
    public float BarPerSec { get; private set; }
    public float BeatPerSec { get; private set; }

    public int BarPerMilliSec { get; private set; }
    public int BeatPerMilliSec { get; private set; }

    public void Init()
    {
        BarPerMilliSec = (int)(signature[0] / (bpm / 60f) * 1000);
        BeatPerMilliSec = BarPerMilliSec / 64;

        BarPerSec = BarPerMilliSec * 0.001f;
        BeatPerSec = BarPerMilliSec / 64f;
    }
}