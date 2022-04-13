using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    public GameObject thisPauseMenu;
    public GameObject optionsMenu;

    bool optionsOn;

    public void prepare(){
       optionsOn = false; 
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape) && optionsOn == false){

           if (gameIsPaused){
               resume();
           }

           else{
               pause();
           }

       }

    }

    public void resume(){
        thisPauseMenu.SetActive(false);
        gameIsPaused = false;
    }

    void pause(){
        thisPauseMenu.SetActive(true);
        gameIsPaused = true;
    }

    public void quiteGame(){
        Debug.Log("quiting");
        Application.Quit();
    }

    public void optionsPress(){
        thisPauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        optionsOn = true;
    }


}
