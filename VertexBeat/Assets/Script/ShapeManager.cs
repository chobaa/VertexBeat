using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeManager : MonoBehaviour
{
    Note theNote;
    TimingManager theTimingManager;

    Image currentImage; // 현재 다룰 도형의 이미지
    [SerializeField] GameObject[] target; // note의 다음 목적지
    int currentShape = 8; // 각 state별로 숫자를 부여 2 = line, 3 = triangle, 4 = square , 5 = pentagon, 6 = hexagon, 8 = octagon
    int target_idx; // 현재 target의 index
    bool changeShape; // 도형이 바뀌어야 할 때 true, 아니면 false

    // Start is called before the first frame update
    void Start()
    {
        theNote = FindObjectOfType<Note>();
        theTimingManager = FindObjectOfType<TimingManager>();
        changeShape = true; // 처음에는 도형이 정해져있지 않으므로 도형 가져오기
        target_idx = 1; // 처음 위치는 도형의 맨 윗 꼭짓점으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        if(changeShape){ // 도형 변환시
            SetTargetTransform(currentShape);
            // 애니메이션 추가, 노트의 변환 순서를 담을 배열 만들기 필요.
            currentImage = target[0].GetComponent<Image>(); // 해당하는 도형의 이미지 가져오기
            currentImage.enabled = true;
            changeShape = false;
        }
        else{
            // 도형 변환을 안해도 되면 NoteMove 호출
            theNote.NoteMove(target, ref target_idx, currentShape);
            // 일단 input도 여기서 처리 (나중에 분리할지 고민좀 해봐야 할듯)
            if(Input.GetKeyDown(KeyCode.Space)){
                if(target_idx == 0 || target_idx==1){
                    theTimingManager.CheckTiming(target[1], theNote.noteImage, target[2]);
                }
                else
                    theTimingManager.CheckTiming(target[target_idx-1], theNote.noteImage, target[target_idx]);
            }
        }
    }

   void SetTargetTransform(int currentShape){ // 도형 변환 함수
        if(currentShape == 2){
            target = GameObject.FindGameObjectsWithTag("Line");
        }
        else if(currentShape == 3){
            target = GameObject.FindGameObjectsWithTag("Triangle");
        }
        else if(currentShape == 4){
            target = GameObject.FindGameObjectsWithTag("Square");
        }
        else if(currentShape == 5){
            target = GameObject.FindGameObjectsWithTag("Pentagon");
        }
        else if(currentShape == 6){
            target = GameObject.FindGameObjectsWithTag("Hexagon");
        }
        else if(currentShape == 8){
            target = GameObject.FindGameObjectsWithTag("Octagon");
        }
    }
}
