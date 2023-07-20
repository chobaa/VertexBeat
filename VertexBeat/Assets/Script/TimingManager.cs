using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimingManager : MonoBehaviour
{

    [SerializeField] float[] timingBoxs; // 판정 체크, 3 = perfect, 2 = good, 3 = pass, 0 = miss 판정으로 구성.
    [SerializeField] TextMeshProUGUI scoretext;
    public int score = 0;
    void Start()
    {
        timingBoxs[3] = 10f;
        timingBoxs[2] = 20f;
        timingBoxs[1] = 60f;
        timingBoxs[0] = 120f;
        scoretext = FindObjectOfType<TextMeshProUGUI>();
    }

    void Update(){
        scoretext.text = $"Score : {score}";
    }

    public bool CheckTiming(GameObject before, GameObject current, GameObject next)
    {
        float distance1 = Vector2.Distance(before.transform.position, current.transform.position); // 이전 목표의 꼭짓점과 현재 노트와의 거리
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
        return false;
    }
}
