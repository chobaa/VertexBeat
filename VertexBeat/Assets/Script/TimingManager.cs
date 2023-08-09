using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimingManager : MonoBehaviour
{
    [SerializeField] double[] timingBoxs; // 판정 체크, 3 = perfect, 2 = good, 3 = pass, 0 = miss 판정으로 구성.
    [SerializeField] TextMeshProUGUI scoretext;
    public int score = 0;
    public double noteSample = 0;
    bool sample_load = false;

    void Update()
    {
        if (!sample_load && GameManager.instance.data_load && GameManager.instance.sync_load)
        {
            noteSample = SongData.instance.firstNoteSample;
            timingBoxs[0] = noteSample * 0.2;
            timingBoxs[1] = noteSample * 0.5;
            timingBoxs[2] = noteSample * 0.8;
            sample_load = true;
        }
        scoretext.text = $"Score : {score}";
    }

    public bool CheckTiming()
    {
        double distance = noteSample - SongData.instance.currentPlayTime;
        if (distance < 0) distance *= -1;
        for (int i = 2; i >= 0; i--)
        {
            if (distance < timingBoxs[i])
            {
                score += i * 10;
                noteSample += NoteData.instance.beatPerSample * 2;
                return true;
            }
        }
        return false;
    }


    /*  float distance1 = Vector2.Distance(before.transform.position, current.transform.position); // 이전 목표의 꼭짓점과 현재 노트와의 거리
        float distance2 = Vector2.Distance(current.transform.position, next.transform.position); // 목표의 꼭짓점과 현재 노트와의 거리
        if (distance1 > distance2) distance1 = distance2; // 더 짧은 것으로 설정해서 판정에 적용
        for (int i = 3; i >= 0; i--)
        {
            if (distance1 < timingBoxs[i])
            {
                score += i * 10;
                return true;
            }
        }
        return false; */
}
