using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour{
    public SongData songData;

    public SongData getSongData() {
        return songData;
    }
    
    void Awake(){
        SongDataLoad("test");
    }

    public void SongDataLoad(string name){
        string FilePath = $"{Application.dataPath}/Resources";
        string readLine = string.Empty;
        NoteData theNoteData = gameObject.AddComponent<NoteData>();
        StreamReader sr = new StreamReader($"{FilePath}/{name}.txt");
        while(!sr.EndOfStream){
            readLine = sr.ReadLine();
            if(readLine.StartsWith('#')){
                string[] data = readLine.Split(" ");
                if(data[0] == "#Title"){
                    Debug.Log(data[1]);
                    Debug.Log(data[2]);
                }
                else if(data[0].IndexOf(":") == 4){ // 데이터 섹션 읽기
                    int time = 0;
                    Int32.TryParse(data[0].Trim().Substring(1,3), out time);
                    string noteStr = data[0].Trim().Substring(5);
                    List<Tuple<int, float>> noteDataList = getNoteDataOfStr(time, noteStr);
                    theNoteData.setNoteDataList(noteDataList);
                }
            }
        }
    }

    private List<Tuple<int, float>> getNoteDataOfStr(int time, string str){
        string tempStr = str.Trim(); // note의 정보를 갖고있는 string
        List<Tuple<int, float>> noteDataList = new List<Tuple<int, float>>();

        int totalHitCount = 0;
        int totalFigureCount = 0;
        int key = 0;
        for(int i=0; i<tempStr.Length; i += 2){ // 각 note를 읽어들이고
            key = (tempStr[i] - '0') * 10;
            key += tempStr[i+1] - '0';
            totalHitCount += key/10; // 각 도형의 꼭짓점의 갯수 (타격판정)
            totalFigureCount++; // 전체 도형의 갯수

            Tuple<int,float> noteData = new Tuple<int,float>(key, (float)time);
            // 시간은 일단 각 도형마다 1초로 계산했지만 이후 bpm 추가해서 세부 조정 필요.
            //songData.totalPlayTime = time;
            noteDataList.Add(noteData);
            //songData.totalNoteCount = totalHitCount;
        }

        return noteDataList;
    }
}