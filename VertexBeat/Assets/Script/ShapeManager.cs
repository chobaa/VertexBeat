using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShapeManager : MonoBehaviour
{
    Note theNote;
    TimingManager theTimingManager;
    AnimManager theAnimManager;
    NoteManager theNoteManager;
    DataManager theDataManager;

    AudioSource audioSource;

    // Image beforeImage = null;
    // Image currentImage = null; // 현재 다룰 도형의 이미지
    [SerializeField] GameObject[] target; // note의 다음 목적지
    public int beforeShape = 0; // 이전 도형 저장
    public int currentShape = 0; // 각 state별로 숫자를 부여 2 = line, 3 = triangle, 4 = square , 5 = pentagon, 6 = hexagon, 8 = octagon
    int target_idx; // 현재 target의 index
    public bool changeShape; // 도형이 바뀌어야 할 때 true, 아니면 false
    [SerializeField] int noteData_idx = 0; // 노트가 바뀌는 순서

    [SerializeField] bool isChecked = false; // 노트가 클릭되면 true, isPassed가 활성화되어 노트가 지나가면 false

    [SerializeField] bool isPassed = false; // 노트가 꼭짓점을 지나가면 true; isChecked를 확인하고나면 다시 false

    // Start is called before the first frame update
    void Start()
    {
        theNote = FindObjectOfType<Note>();
        theTimingManager = FindObjectOfType<TimingManager>();
        theAnimManager = FindObjectOfType<AnimManager>();
        theDataManager = FindObjectOfType<DataManager>();

        audioSource = GetComponent<AudioSource>();
        changeShape = true; // 처음에는 도형이 정해져있지 않으므로 도형 가져오기
        target_idx = 1; // 처음 위치는 도형의 맨 윗 꼭짓점으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            if (!GameManager.instance.data_load)
            {
                theDataManager.SongDataLoad("test");
                GameManager.instance.data_load = true;
            }
            else
            {
                if (changeShape)
                { // 도형 변환시
                    ChangingShape();
                    changeShape = false;
                }
                else
                {
                    // if (isPassed && !isChecked) Debug.Log("GameOver"); // 판정범위를 지나갔을 때 good / pass가 뜨지 않으면 GameOver
                    // 도형 변환을 안해도 되면 NoteMove 호출
                    theNote.NoteMove(target, ref target_idx, currentShape, ref isPassed, ref changeShape, NoteData.instance.target_cnt);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isPassed = false;
                        audioSource.PlayOneShot(audioSource.clip);
                        if (target_idx == 0 || target_idx == 1)
                        {
                            isChecked = theTimingManager.CheckTiming();
                        }
                        else
                            isChecked = theTimingManager.CheckTiming();
                    }
                }
            }
        }
    }

    void SetTargetTransform(int currentShape)
    { // 도형 변환 함수
        if (currentShape == 31)
        {
            target = GameObject.FindGameObjectsWithTag("Triangle_112");
            theNote.noteSpeed = 9;
        }
        else if (currentShape == 32)
        {
            target = GameObject.FindGameObjectsWithTag("Triangle_121");
            theNote.noteSpeed = 9;
        }
        else if (currentShape == 33)
        {
            target = GameObject.FindGameObjectsWithTag("Triangle_211");
            theNote.noteSpeed = 9;
        }
        else if (currentShape == 34)
        {
            target = GameObject.FindGameObjectsWithTag("Triangle_05152");
            theNote.noteSpeed = 9;
        }
        else if (currentShape == 41)
        {
            target = GameObject.FindGameObjectsWithTag("Square_1111");
            theNote.noteSpeed = 15;
            NoteData.instance.target_cnt = 2;
        }
        else if (currentShape == 42)
        {
            target = GameObject.FindGameObjectsWithTag("Square_051511");
            theNote.noteSpeed = 9;
        }
        else if (currentShape == 51)
        {
            target = GameObject.FindGameObjectsWithTag("Pentagon_1105105");
            theNote.noteSpeed = 9;
        }
        else if (currentShape == 61)
        {
            target = GameObject.FindGameObjectsWithTag("Hexagon_1105050505");
            theNote.noteSpeed = 9;
        }
        else if (currentShape == 81)
        {
            target = GameObject.FindGameObjectsWithTag("Octagon_0505050505050505");
            NoteData.instance.target_cnt = 1;
            theNote.noteSpeed = 3;
        }
    }

    void ChangingShape()
    {
        beforeShape = currentShape;
        List<Tuple<int, float>> noteData = NoteData.instance.getNoteDataList();
        currentShape = noteData[noteData_idx++].Item1;
        Debug.Log("Shape : " + currentShape);
        Debug.Log(noteData_idx);
        SetTargetTransform(currentShape);
        if (currentShape != beforeShape)
        { // 이미지가 변했을 때에만 애니메이션 재생
            if (beforeShape != 0)
            { // 이전에 도형이 존재했다면 FadeOut 애니메이션 재생
                // beforeImage.enabled = false;
                theAnimManager.FadeOut_Animation(beforeShape);
                Debug.Log("Animation FadeOut");
            }
            // 현재 도형에 대한 Bigger 애니메이션 재생
            Debug.Log("Animation Bigger");
            theAnimManager.Bigger_Animation(currentShape);
        }
    }

    /* bool CheckPassNote(){
        if (isPassed) // GameOver처리, 박자 별 Animation 실행
            {
            if (Vector2.Distance(theNote.Cursor.transform.position, target[target_idx].transform.position) > 80f)
            {
                if (isChecked)
                {
                    isChecked = false;
                    isPassed = false;
                    return true;
                }
                else
                   return false;
            }
         }
         return false;
    } */
}
