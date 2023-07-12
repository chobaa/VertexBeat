using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShapeManager : MonoBehaviour
{
    Note theNote;
    TimingManager theTimingManager;
    NoteData theNoteData;
    [SerializeField] Animator TriangleAnim = null;
    [SerializeField] Animator SquareAnim = null;
    [SerializeField] Animator HexagonAnim = null;
    [SerializeField] Animator OctagonAnim = null;
    [SerializeField] AudioSource audioSource;

    string FadeOut = "FadeOut";
    string Bigger = "Bigger";

    // Image beforeImage = null;
    // Image currentImage = null; // 현재 다룰 도형의 이미지
    [SerializeField] GameObject[] target; // note의 다음 목적지
    public int beforeShape = 0; // 이전 도형 저장
    public int currentShape = 0; // 각 state별로 숫자를 부여 2 = line, 3 = triangle, 4 = square , 5 = pentagon, 6 = hexagon, 8 = octagon
    int target_idx; // 현재 target의 index
    public bool changeShape; // 도형이 바뀌어야 할 때 true, 아니면 false
    [SerializeField] int noteData_idx = 0; // 노트가 바뀌는 순서

    bool isChecked = true; // 노트가 클릭되면 true, isPassed가 활성화되어 노트가 지나가면 false

    bool isPassed = false; // 노트가 꼭짓점을 지나가면 true; isChecked를 확인하고나면 다시 false

    // Start is called before the first frame update
    void Start()
    {
        theNote = FindObjectOfType<Note>();
        theTimingManager = FindObjectOfType<TimingManager>();
        theNoteData = FindObjectOfType<NoteData>();
        audioSource = FindObjectOfType<AudioSource>();
        changeShape = true; // 처음에는 도형이 정해져있지 않으므로 도형 가져오기
        target_idx = 1; // 처음 위치는 도형의 맨 윗 꼭짓점으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (changeShape)
        { // 도형 변환시
            ChangingShape();
            changeShape = false;
        }
        else
        {
            CheckPassNote();
            // 도형 변환을 안해도 되면 NoteMove 호출
            audioSource.Play();
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
            theNote.noteSpeed = 9;
        }
        else if (currentShape == 4)
        {
            target = GameObject.FindGameObjectsWithTag("Square");
            theNote.noteSpeed = 12;
        }
        else if (currentShape == 5)
        {
            target = GameObject.FindGameObjectsWithTag("Pentagon");
            theNote.noteSpeed = 15;
        }
        else if (currentShape == 6)
        {
            target = GameObject.FindGameObjectsWithTag("Hexagon");
            theNote.noteSpeed = 18;
        }
        else if (currentShape == 8)
        {
            target = GameObject.FindGameObjectsWithTag("Octagon");
            theNote.noteSpeed = 24;
        }
    }

    void ChangingShape(){
        beforeShape = currentShape;
        List<Tuple<int,float>> noteData = theNoteData.getNoteDataList();
        currentShape = noteData[noteData_idx++].Item1;
        Debug.Log(currentShape);
        Debug.Log(noteData_idx);
        SetTargetTransform(currentShape);
        if(currentShape != beforeShape){ // 이미지가 변했을 때에만 애니메이션 재생
            // currentImage = target[0].GetComponent<Image>(); // 해당하는 도형의 이미지 가져오기
            if(beforeShape != 0) { // 이전에 도형이 존재했다면 FadeOut 애니메이션 재생
                // beforeImage.enabled = false;
                FadeOut_Animation(beforeShape);
                Debug.Log("Animation FadeOut");
                }
            // 현재 도형에 대한 Bigger 애니메이션 재생
            Debug.Log("Animation Bigger");
            // currentImage.enabled = true;
            Bigger_Animation(currentShape);
        }
    }

    bool CheckPassNote(){
        if (isPassed) // GameOver처리, 박자 별 Animation 실행
            {
            if (Vector2.Distance(theNote.noteImage.transform.position, target[target_idx].transform.position) > 80f)
            {
                if (isChecked)
                {
                    isChecked = false;
                    isPassed = false;
                    return true;
                }
                else
                   //Debug.Log("GameOver");
                   return false;
            }
         }
         return false;
    }

    void FadeOut_Animation(int beforeShape){
        if(beforeShape == 3){
            TriangleAnim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 4){
            SquareAnim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 6){
            HexagonAnim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 8){
            OctagonAnim.SetTrigger(FadeOut);
        }
    }

    void Bigger_Animation(int currentShape){
        if(currentShape == 3){
            TriangleAnim.SetTrigger(Bigger);
        }
        else if(currentShape == 4){
            SquareAnim.SetTrigger(Bigger);
        }
        else if(currentShape == 6){
            HexagonAnim.SetTrigger(Bigger);
        }
        else if(currentShape == 8){
            OctagonAnim.SetTrigger(Bigger);
        }
    }

    /*void NoteClick_Animation(int beforeShape){
        if(beforeShape == 3){
            TriangleAnim.SetTrigger(Clicked);
        }
        else if(beforeShape == 4){
            SquareAnim.SetTrigger(Clicked);
        }
        else if(beforeShape == 6){
            HexagonAnim.SetTrigger(Clicked);
        }
        else if(beforeShape == 8){
            OctagonAnim.SetTrigger(Clicked);
        }
    }*/
}
