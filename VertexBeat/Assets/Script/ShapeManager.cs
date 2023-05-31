using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeManager : MonoBehaviour
{
    Note theNote;
    TimingManager theTimingManager;
    NoteManager theNoteManager;

    Image beforeImage; // 이전 도형의 이미지
    Image currentImage; // 현재 다룰 도형의 이미지
    [SerializeField] GameObject[] target; // note의 다음 목적지
    public int beforeShape = 0; // 이전 도형 저장
    public int currentShape = 8; // 각 state별로 숫자를 부여 2 = line, 3 = triangle, 4 = square , 5 = pentagon, 6 = hexagon, 8 = octagon
    int target_idx; // 현재 target의 index
    public bool changeShape; // 도형이 바뀌어야 할 때 true, 아니면 false
    int noteInfo_idx = 0; // 노트가 바뀌는 순서

    bool isChecked = true; // 노트가 클릭되면 true, isPassed가 활성화되어 노트가 지나가면 false

    bool isPassed = false; // 노트가 꼭짓점을 지나가면 true; isChecked를 확인하고나면 다시 false

    // Start is called before the first frame update
    void Start()
    {
        theNote = FindObjectOfType<Note>();
        theTimingManager = FindObjectOfType<TimingManager>();
        theNoteManager = FindObjectOfType<NoteManager>();
        changeShape = true; // 처음에는 도형이 정해져있지 않으므로 도형 가져오기
        target_idx = 1; // 처음 위치는 도형의 맨 윗 꼭짓점으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (changeShape)
        { // 도형 변환시
            beforeShape = currentShape;
            currentShape = theNoteManager.noteInfo[noteInfo_idx++];
            SetTargetTransform(currentShape);
            // 애니메이션 추가, 노트의 변환 순서를 담을 배열 만들기 필요.
            if(currentImage) { // 이전에 도형이 존재했다면 이미지 끄기
                beforeImage = currentImage;
                beforeImage.enabled = false;
            }
            currentImage = target[0].GetComponent<Image>(); // 해당하는 도형의 이미지 가져오기
            currentImage.enabled = true; // 새로 받아온 도형의 이미지 on
            changeShape = false;
        }
        else
        {
            if (isPassed) // GameOver처리
            {
                if (Vector2.Distance(theNote.noteImage.transform.position, target[target_idx].transform.position) > 80f)
                {
                    if (isChecked)
                    {
                        isChecked = false;
                        isPassed = false;
                    }
                    else
                    {
                        Debug.Log("GameOver");
                    }
                }
            }
            // 도형 변환을 안해도 되면 NoteMove 호출
            theNote.NoteMove(target, ref target_idx, currentShape, ref isPassed, ref changeShape);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (target_idx == 0 || target_idx == 1)
                {
                    isChecked = theTimingManager.CheckTiming(target[1], theNote.noteImage, target[2]);
                }
                else
                    isChecked = theTimingManager.CheckTiming(target[target_idx - 1], theNote.noteImage, target[target_idx]);
            }
        }
    }

    void SetTargetTransform(int currentShape)
    { // 도형 변환 함수
        if (currentShape == 2)
        {
            target = GameObject.FindGameObjectsWithTag("Line");
        }
        else if (currentShape == 3)
        {
            target = GameObject.FindGameObjectsWithTag("Triangle");
        }
        else if (currentShape == 4)
        {
            target = GameObject.FindGameObjectsWithTag("Square");
        }
        else if (currentShape == 5)
        {
            target = GameObject.FindGameObjectsWithTag("Pentagon");
        }
        else if (currentShape == 6)
        {
            target = GameObject.FindGameObjectsWithTag("Hexagon");
        }
        else if (currentShape == 8)
        {
            target = GameObject.FindGameObjectsWithTag("Octagon");
        }
    }
}
