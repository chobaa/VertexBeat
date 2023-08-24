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

    [SerializeField] GameObject[] target; // note의 다음 목적지
    public List<Tuple<int, float>> noteData; // note 정보 저장 list
    public int beforeShape = 0; // 이전 도형 저장
    public int currentShape = 0; // 각 state별로 숫자를 부여 2 = line, 3 = triangle, 4 = square , 5 = pentagon, 6 = hexagon, 8 = octagon
    public int nextShape = 0;
    [SerializeField] int target_idx; // 현재 target의 index
    public bool changeShape; // 도형이 바뀌어야 할 때 true, 아니면 false
    [SerializeField] int noteData_idx = 0; // 노트가 바뀌는 순서

    [SerializeField] bool isChecked = false; // 노트가 클릭되면 true, isPassed가 활성화되어 노트가 지나가면 false

    [SerializeField] bool isPassed = false; // 노트가 꼭짓점을 지나가면 true; isChecked를 확인하고나면 다시 false

    // 애니메이션에서 사용되는 변수
    public Image beforeImage = null;
    public Image currentImage = null;
    public Image nextImage = null;
    public bool isFadein = false;
    public bool isFadeout = false;

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
                noteData = NoteData.instance.getNoteDataList();
                ChangingShape();
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
                    if (target_idx == 1 && isFadein)
                    {
                        StartCoroutine(FadeIn(NoteData.instance.oneBeatTime * 8, nextImage, 0, 1));
                        isFadein = false;
                        isFadeout = true;
                    }
                    theNote.NoteMove(target, ref target_idx, currentShape, ref isPassed, ref changeShape);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isPassed = false;
                        //audioSource.PlayOneShot(audioSource.clip);
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
            NoteData.instance.target_cnt[0] = 2;
            NoteData.instance.target_cnt[1] = 2;
            NoteData.instance.target_cnt[2] = 4;
        }
        else if (currentShape == 32)
        {
            target = GameObject.FindGameObjectsWithTag("Triangle_121");
            NoteData.instance.target_cnt[0] = 2;
            NoteData.instance.target_cnt[1] = 4;
            NoteData.instance.target_cnt[2] = 2;
        }
        else if (currentShape == 33)
        {
            target = GameObject.FindGameObjectsWithTag("Triangle_211");
            NoteData.instance.target_cnt[0] = 4;
            NoteData.instance.target_cnt[1] = 2;
            NoteData.instance.target_cnt[2] = 2;
        }
        else if (currentShape == 34)
        {
            target = GameObject.FindGameObjectsWithTag("Triangle_05152");
            NoteData.instance.target_cnt[0] = 1;
            NoteData.instance.target_cnt[1] = 3;
            NoteData.instance.target_cnt[2] = 4;
        }
        else if (currentShape == 41)
        {
            target = GameObject.FindGameObjectsWithTag("Square_1111");

            NoteData.instance.target_cnt[0] = 2;
            NoteData.instance.target_cnt[1] = 2;
            NoteData.instance.target_cnt[2] = 2;
            NoteData.instance.target_cnt[3] = 2;
        }
        else if (currentShape == 42)
        {
            target = GameObject.FindGameObjectsWithTag("Square_051511");
            NoteData.instance.target_cnt[0] = 1;
            NoteData.instance.target_cnt[1] = 3;
            NoteData.instance.target_cnt[2] = 2;
            NoteData.instance.target_cnt[3] = 2;
        }
        else if (currentShape == 51)
        {
            target = GameObject.FindGameObjectsWithTag("Pentagon_1105105");
            NoteData.instance.target_cnt[0] = 2;
            NoteData.instance.target_cnt[1] = 2;
            NoteData.instance.target_cnt[2] = 1;
            NoteData.instance.target_cnt[3] = 2;
            NoteData.instance.target_cnt[4] = 1;
        }
        else if (currentShape == 61)
        {
            target = GameObject.FindGameObjectsWithTag("Hexagon_1105050505");
            NoteData.instance.target_cnt[0] = 2;
            NoteData.instance.target_cnt[1] = 2;
            NoteData.instance.target_cnt[2] = 1;
            NoteData.instance.target_cnt[3] = 1;
            NoteData.instance.target_cnt[4] = 1;
            NoteData.instance.target_cnt[5] = 1;
        }
        else if (currentShape == 81)
        {
            target = GameObject.FindGameObjectsWithTag("Octagon_0505050505050505");
            NoteData.instance.target_cnt[0] = 1;
            NoteData.instance.target_cnt[1] = 1;
            NoteData.instance.target_cnt[2] = 1;
            NoteData.instance.target_cnt[3] = 1;
            NoteData.instance.target_cnt[4] = 1;
            NoteData.instance.target_cnt[5] = 1;
            NoteData.instance.target_cnt[6] = 1;
            NoteData.instance.target_cnt[7] = 1;
        }
    }

    void ChangingShape()
    {
        // 기존 값을 저장
        beforeImage = currentImage;
        currentImage = nextImage;
        beforeShape = currentShape;
        currentShape = nextShape;
        // 새로 데이터를 받아옴
        nextShape = noteData[noteData_idx++].Item1;
        Debug.Log("Shape : " + nextShape);
        Debug.Log(noteData_idx);
        // 현재값과 다음 데이터 값의 좌표를 저장
        SetTargetTransform(currentShape);
        GetNextImage(nextShape);
        if (beforeShape == 0 && currentShape != 0) currentImage.enabled = true;
        if (currentShape != 0 && nextShape != currentShape) isFadein = true;
        if (isFadeout)
        {
            StartCoroutine(FadeOut(NoteData.instance.oneBeatTime / 2, beforeImage, 1, 0));
            isFadeout = false;
        }
    }


    void GetNextImage(int nextShape)
    {
        if (nextShape == 31)
        {
            nextImage = GameObject.Find("Triangle_112").GetComponent<Image>();
        }
        else if (nextShape == 32)
        {
            nextImage = GameObject.Find("Triangle_121").GetComponent<Image>();
        }
        else if (nextShape == 33)
        {
            nextImage = GameObject.Find("Triangle_211").GetComponent<Image>();
        }
        else if (nextShape == 34)
        {
            nextImage = GameObject.Find("Triangle_05152").GetComponent<Image>();
        }
        else if (nextShape == 41)
        {
            nextImage = GameObject.Find("Square_1111").GetComponent<Image>();
        }
        else if (nextShape == 42)
        {
            nextImage = GameObject.Find("Square_051511").GetComponent<Image>();
        }
        else if (nextShape == 51)
        {
            nextImage = GameObject.Find("Pentagon_1105105").GetComponent<Image>();
        }
        else if (nextShape == 61)
        {
            nextImage = GameObject.Find("Hexagon_1105050505").GetComponent<Image>();
        }
        else if (nextShape == 81)
        {
            nextImage = GameObject.Find("Octagon_0505050505050505").GetComponent<Image>();
        }
    }
    IEnumerator FadeOut(double duration, Image beforeImage, float start, float end)
    {
        var runTime = 0.0f;
        /*
        while (runTime < (float)duration)
        {
            runTime += Time.deltaTime;

            beforeImage.color = new Color(beforeImage.color.r, beforeImage.color.g, beforeImage.color.b, Mathf.Lerp(start, end, runTime / (float)duration));

            yield return null;
        }
        */

        beforeImage.enabled = false;

        yield return null;
    }
    IEnumerator FadeIn(double duration, Image currentImage, float start, float end)
    {
        var runTime = 0.0f;

        currentImage.enabled = true;

        while (runTime < (float)duration)
        {
            runTime += Time.deltaTime;

            currentImage.transform.localScale = new Vector3(Mathf.Lerp(start, end, runTime / (float)duration), Mathf.Lerp(start, end, runTime / (float)duration), 1);

            currentImage.color = new Color(currentImage.color.r, currentImage.color.g, currentImage.color.b, Mathf.Lerp(start, 0.5f, runTime / (float)duration));

            yield return null;
        }

        currentImage.color = new Color(currentImage.color.r, currentImage.color.g, currentImage.color.b, 1f);

        currentImage.transform.localScale = new Vector3(1, 1, 1);
    }
}
