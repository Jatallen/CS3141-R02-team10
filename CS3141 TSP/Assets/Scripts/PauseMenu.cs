using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject Canvas;

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape)){

           if (gameIsPaused){
               resume();
           }

           else{
               pause();
           }

       }

    }

    public void resume(){
        Canvas.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void pause(){
        Canvas.SetActive(true);
        //Time.timeScale = 0f; its chess and tyler will complain so 
        gameIsPaused = true;
    }

    public void quiteGame(){
        Debug.Log("quiting");
        Application.Quit();
    }
}
