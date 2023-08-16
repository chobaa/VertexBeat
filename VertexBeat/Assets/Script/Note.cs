using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    Sync theSync;
    ShapeManager theShapeManager;
    [SerializeField] public GameObject Cursor = null;
    public float[] noteSpeed; // 노트의 기본 이동속도
    public int bpm = 1; // 노래의 bpm
    public int cnt = 0;

    private Vector2 start_pos;
    private bool isMoveStart;

    void Start()
    {
        theSync = FindObjectOfType<Sync>();
        theShapeManager = FindObjectOfType<ShapeManager>();
        Cursor = GameObject.Find("Cursor");
        start_pos = Cursor.transform.position;
        isMoveStart = true;
    }

    public void NoteMove(GameObject[] target, ref int target_idx, int currentShape, ref bool isPassed, ref bool changeShape)
    {
        //Cursor.transform.position = Vector2.MoveTowards(Cursor.transform.position, target[target_idx].transform.position, bpm * noteSpeed[target_idx - 1] * Time.deltaTime); // 노트의 움직임은 deltatime
        if (isMoveStart)
        {
            StartCoroutine(Run(NoteData.instance.oneBeatTime * NoteData.instance.target_cnt[target_idx - 1], target, target_idx));
            isMoveStart = false;
        }
        if (NoteData.instance.isNextNote) // 1/8 박자마다 메트로놈 작동
        {
            cnt++; // cnt 변수를 이용해 1/4 , 1/2 , 1박자 표현 가능
            NoteData.instance.isNextNote = false;
        }
        if (NoteData.instance.target_cnt[target_idx - 1] == cnt) // target_cnt에 도달하면 박자 재생
        {
            Cursor.transform.position = target[target_idx].transform.position;
            start_pos = target[target_idx].transform.position;
            isMoveStart = true;
            target_idx += 1;
            isPassed = true;
            cnt = 0;
        }

        if (target_idx == currentShape / 10 + 1) // 다음 도형으로 넘어가는 상황
        {
            target_idx = 1;
            isMoveStart = true;
            changeShape = true;
            isPassed = true;
        }
    }

    IEnumerator isDelay()
    {
        yield return new WaitForSeconds(0.1f); // 0.1초 동안 distance 측정 x
    }

    IEnumerator Run(double duration, GameObject[] target, int target_idx)
    {
        var runTime = 0.0f;

        while (runTime < (float)duration)
        {
            runTime += Time.deltaTime;

            Cursor.transform.position = Vector2.Lerp(start_pos, target[target_idx].transform.position, runTime / (float)duration);

            yield return null;
        }
    }

    /*float distance = Vector2.Distance(Cursor.transform.position, target[target_idx].transform.position);
        if (distance < 0.01)
        {
            target_idx += 1;
            StartCoroutine(isDelay());
            isPassed = true;
        }
        if (target_idx == currentShape/10 + 1)
        {
            target_idx = 1;
            changeShape = true;
            isPassed = true;
        }*/
}
