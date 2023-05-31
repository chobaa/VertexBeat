using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    ShapeManager theShapeManager;
    [SerializeField] public GameObject noteImage = null;
    float noteSpeed = 5f; // 노트의 기본 이동속도
    [SerializeField] int bpm = 1; // 노래의 bpm

    void Start()
    {
        theShapeManager = FindObjectOfType<ShapeManager>();
        noteImage = GameObject.Find("NoteImage");
    }

    public void NoteMove(GameObject[] target, ref int target_idx, int currentShape, ref bool isPassed, ref bool changeShape)
    {
        noteImage.transform.position = Vector2.MoveTowards(noteImage.transform.position, target[target_idx].transform.position, bpm * noteSpeed * Time.deltaTime);
        float distance = Vector2.Distance(noteImage.transform.position, target[target_idx].transform.position);
        if (distance < 0.01)
        {
            target_idx += 1;
            isPassed = true;
        }
        if (target_idx == currentShape + 1)
        {
            target_idx = 1;
            isPassed = true;
            changeShape = true;
        }
    }
}
