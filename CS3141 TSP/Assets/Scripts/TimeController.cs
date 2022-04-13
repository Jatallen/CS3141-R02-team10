using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour
{
    Text WhiteText,BlackText;
    public static float timeLeftWhite = 60;
    public static float timeLeftBlack = 60;

    // Start is called before the first frame update
    void Start()
    {
        WhiteText = GameObject.Find("WhiteTime").GetComponent<Text>();
        BlackText = GameObject.Find("BlackTime").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan t;
        if (GameController.turn.Equals("White")){
            timeLeftWhite -= Time.deltaTime/2;
            t = TimeSpan.FromSeconds( timeLeftWhite );
            string str;
            if (timeLeftWhite > 3600){
                str = t.ToString(@"h:\mm\:ss");}
            else if (timeLeftWhite > 600){
                str = t.ToString(@"mm\:ss");}
            else if (timeLeftWhite > 60){
                str = t.ToString(@"m\:ss");}
            else {
                str = t.ToString(@"ss\:ff");}
            
            WhiteText.text = str;
            
            }
        else if (GameController.turn.Equals("Black")){
            timeLeftBlack -= Time.deltaTime/2;
            t = TimeSpan.FromSeconds( timeLeftBlack );
            
            string str;
            if (timeLeftBlack > 3600){
                str = t.ToString(@"h:\mm\:ss");}
            else if (timeLeftBlack > 600){
                str = t.ToString(@"mm\:ss");}
            else if (timeLeftBlack > 60){
                str = t.ToString(@"m\:ss");}
            else {
                str = t.ToString(@"ss\:ff");}
            
            BlackText.text = str;
        }
        if (timeLeftWhite <= 0)
            GameController.GameOver("White");
        if (timeLeftBlack <= 0)
            GameController.GameOver("Black");
    }
    
}
