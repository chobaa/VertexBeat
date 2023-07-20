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
    AudioManager theAudioManager;
    //Triangle
    [SerializeField] Animator Triangle_121_Anim = null;
    [SerializeField] Animator Triangle_112_Anim = null;
    [SerializeField] Animator Triangle_211_Anim = null;
    [SerializeField] Animator Triangle_05152_Anim = null;
    //Square
    [SerializeField] Animator Square_1111_Anim = null;
    [SerializeField] Animator Square_051511_Anim = null;
    //Pentagon
    [SerializeField] Animator Pentagon_1105105_Anim = null;
    [SerializeField] Animator Hexagon_1105050505_Anim = null;
    [SerializeField] Animator Octagon_0505050505050505_Anim = null;

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
        theAudioManager = FindObjectOfType<AudioManager>();
        changeShape = true; // 처음에는 도형이 정해져있지 않으므로 도형 가져오기
        target_idx = 1; // 처음 위치는 도형의 맨 윗 꼭짓점으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        theAudioManager.setStartTrue(); // 노래 재생 bool
        if (changeShape)
        { // 도형 변환시
            ChangingShape();
            changeShape = false;
        }
        else
        {
            CheckPassNote();
            // 도형 변환을 안해도 되면 NoteMove 호출
            theNote.NoteMove(target, ref target_idx, currentShape, ref isPassed, ref changeShape);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (target_idx == 0 || target_idx == 1)
                {
                    isChecked = theTimingManager.CheckTiming(target[1], theNote.Cursor, target[2]);
                }
                else
                    isChecked = theTimingManager.CheckTiming(target[target_idx - 1], theNote.Cursor, target[target_idx]);
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
            theNote.noteSpeed = 9;
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
            theNote.noteSpeed = 9;
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
            if (Vector2.Distance(theNote.Cursor.transform.position, target[target_idx].transform.position) > 80f)
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
        if(beforeShape == 31){
            Triangle_121_Anim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 32){
            Triangle_112_Anim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 33){
            Triangle_211_Anim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 34){
            Triangle_05152_Anim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 41){
            Square_1111_Anim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 42){
            Square_051511_Anim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 51){
            Pentagon_1105105_Anim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 61){
            Hexagon_1105050505_Anim.SetTrigger(FadeOut);
        }
        else if(beforeShape == 8){
            Octagon_0505050505050505_Anim.SetTrigger(FadeOut);
        }
    }

    void Bigger_Animation(int currentShape){
        if(currentShape == 31){
            Triangle_121_Anim.SetTrigger(Bigger);
        }
        else if(currentShape == 32){
            Triangle_112_Anim.SetTrigger(Bigger);
        }
        else if(currentShape == 33){
            Triangle_211_Anim.SetTrigger(Bigger);
        }
        else if(currentShape == 34){
            Triangle_05152_Anim.SetTrigger(Bigger);
        }
        else if(currentShape == 41){
            Square_1111_Anim.SetTrigger(Bigger);
        }
        else if(currentShape == 42){
            Square_051511_Anim.SetTrigger(Bigger);
        }
        else if(currentShape == 51){
            Pentagon_1105105_Anim.SetTrigger(Bigger);
        }
        else if(currentShape == 61){
            Hexagon_1105050505_Anim.SetTrigger(Bigger);
        }
        else if(currentShape == 81){
            Octagon_0505050505050505_Anim.SetTrigger(Bigger);
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
