using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Main Menu");
    }


    public void optionsPress(){
        thisPauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        optionsOn = true;
    }


}
