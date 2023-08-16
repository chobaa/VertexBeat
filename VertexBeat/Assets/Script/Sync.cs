using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : MonoBehaviour
{
    public AudioSource audioSource;

    public double stdBPM;
    public double oneBeatTime;  // 메트로놈 재생 박자
    public double nextSample;
    public double beatPerSecond;
    public double beatPerSample;
    public double beforeSample = 0;

    public int beatcount = 0;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.data_load && !GameManager.instance.sync_load)
        {
            audioSource = GetComponent<AudioSource>();
            stdBPM = 60;
            audioSource.Play();
            oneBeatTime = (stdBPM / SongData.instance.bpm) * SongData.instance.signature / 2;
            nextSample = oneBeatTime * audioSource.clip.frequency;
            beatPerSecond = stdBPM / (8 * SongData.instance.bpm);
            beatPerSample = oneBeatTime * audioSource.clip.frequency;
            NoteData.instance.beatPerSample = beatPerSample;
            NoteData.instance.oneBeatTime = oneBeatTime;
            GameManager.instance.sync_load = true;
        }
        SongData.instance.currentPlayTime = audioSource.timeSamples;
        if (GameManager.instance.sync_load && audioSource.timeSamples >= nextSample)
        {
            NoteData.instance.isNextNote = true;
            StartCoroutine(PlayTik(1));
        }
    }

    IEnumerator PlayTik(double tikTime)
    {
        Debug.Log("tik");
        nextSample += beatPerSample;
        beatcount++;
        yield return null; // tikTime 만큼 대기
    }
}
