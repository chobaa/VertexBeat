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

    bool value_reset = false;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.data_load && !value_reset){
            audioSource = GetComponent<AudioSource>();
            stdBPM = 60;
            audioSource.Play();
            
            oneBeatTime = (stdBPM / SongData.instance.bpm) * SongData.instance.signature;
            nextSample = oneBeatTime * audioSource.clip.frequency;
            beatPerSecond = stdBPM / (8 * SongData.instance.bpm);
            beatPerSample = oneBeatTime * audioSource.clip.frequency;
            value_reset = true;
        }
        if(value_reset && audioSource.timeSamples >= nextSample){
            StartCoroutine(PlayTik(1));
        }
    }

    IEnumerator PlayTik(double tikTime){
        Debug.Log("tik");
        nextSample += beatPerSample;
        SongData.instance.currentPlayTime = nextSample;
        yield return null; // tikTime 만큼 대기
    }
}
