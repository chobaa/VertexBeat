using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Networking;
/*
public class Parser : MonoBehaviour
{
    public Sheet sheet;
    string sheetText = string.Empty;
    string[] textSplit;

    // Start is called before the first frame update
    void Start()
    {
        sheet = new Sheet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ParseSheet(){
        while(!reader.EndOfStream){
            sheetText = reader.readline();
            textSplit = sheetText.split('=');

            if(textSplit[0].Equals("AudioFileName"))
                sheet.AudioFileName = textSplit[1];
            else if(textSplit[0].Equals("AudioRunTime"))
                sheet.AudioRunTime = textSplit[1];
            else if(textSplit[0].Equals("BPM"))
                sheet.Bpm = Single.Parse(textSplit[1]);
            else if(textSplit[0].Equals("Beat"))
                sheet.Beat = Int32.Parse(textSplit[1]);
            else if(textSplit[0].Equals("Title"))
                sheet.Title = textSplit[1];
            else if(textSplit[0].Equals("Artist"))
                sheet.Artist = textSplit[1];
            else if(textSplit[0].Equals("Stage"))
                sheet.Stage = textSplit[1];
            else if(sheetText.Equals("[NoteInfo]")){
                while(!reader.EndOfStream){
                    sheetText = reader.ReadLine();
                    textSplit = sheetText.Split(',');

                }
            }
        }
    }
} 
*/