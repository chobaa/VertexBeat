using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    ShapeManager theShapeManager;
    [SerializeField] public GameObject noteImage = null;
    float noteSpeed = 5f; // 노트의 기본 이동속도
    [SerializeField] int bpm = 1; // 노래의 bpm
    [SerializeField] bool isDelayed = false; // distance 측정 확인 변수 couroutine 사용.

    void Start()
    {
        theShapeManager = FindObjectOfType<ShapeManager>();
        noteImage = GameObject.Find("NoteImage");
    }

    public void NoteMove(GameObject[] target, ref int target_idx, int currentShape, ref bool isPassed, ref bool changeShape)
    {
        noteImage.transform.position = Vector2.MoveTowards(noteImage.transform.position, target[target_idx].transform.position, bpm * noteSpeed * Time.deltaTime);
        float distance = Vector2.Distance(noteImage.transform.position, target[target_idx].transform.position);
        if (distance < 0.01 && !isDelayed)
        {
            target_idx += 1;
            isPassed = true;
            isDelayed = true;
            StartCoroutine(isDelay());
            }
        if (target_idx == currentShape + 1)
        {
            target_idx = 1;
            isPassed = true;
            changeShape = true;
        }
    }

    IEnumerator isDelay(){
        yield return new WaitForSeconds(0.2f); // 0.2초 동안 distance 측정 x
        isDelayed = false;
    }
}
