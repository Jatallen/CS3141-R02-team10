using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionsMenu : MonoBehaviour
{
    public GameObject thisoptionsMenu;
    public GameObject pauseMenu;

    public GameObject allPauseMenu;

    void update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
             exit();
        }

    }

    public void exit(){
        pauseMenu.SetActive(false);
        thisoptionsMenu.SetActive(false);
        allPauseMenu.GetComponent<PauseMenu>().prepare();
        allPauseMenu.GetComponent<PauseMenu>().gameIsPaused = false;
    }

    public void toggleFullscreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }



}


