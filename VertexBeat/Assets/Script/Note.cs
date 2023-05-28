using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    [SerializeField] Transform noteImage = null;
    float noteSpeed = 5f; // 노트의 기본 이동속도
    [SerializeField] int bpm = 1; // 노래의 bpm
    int currentShape = 3; // 각 state별로 숫자를 부여 2 = line, 3 = triangle, 4 = square , 5 = pentagon, 6 = hexagon, 8 = octagon
    int target_idx; // 현재 target의 index
    bool changeShape; // 도형이 바뀌어야 할 때 true, 아니면 false
    [SerializeField] Transform[] target; // note의 다음 목적지
    
    void Start()
    {
        noteImage = transform.Find("NoteImage");
        target[0] = transform.Find("TriangleTransform").Find("General");
        noteImage.position = target[0].position;
        changeShape = true;
    }

    void Update()
    {
        if(changeShape){ // target의 idx에 대한 transform 설정
            SetTargetTransform(transform, currentShape);
            target_idx = 1;
            changeShape = false;
        }
        if(!changeShape)
            NoteMove();
    }

    void SetTargetTransform(Transform current, int currentShape){
        if(currentShape == 3){
            target[1] = current.Find("TriangleTransform").Find("Triangle");
            target[2] = current.Find("TriangleTransform").Find("Triangle2");
        }
    }

    public void NoteMove(){
        noteImage.position = Vector2.MoveTowards(noteImage.position, target[target_idx].position, bpm * noteSpeed * Time.deltaTime);
        if(noteImage.position == target[target_idx].position){ // 아직 해결 못햇음
            target_idx += 1;
        }
        if(target_idx == currentShape){
            target_idx = 0;
        }
    }
}
