using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    
    //Go from main menu to log in page
    public void LogInButton(){
        SceneManager.LoadScene("Log In");
    }

    //Go from login page to chess board
    public void LogInButton2()
    {
        SceneManager.LoadScene("ChessBoard");
    }

    //Go from main menu to the sign up page
    public void SignUpButton(){
        SceneManager.LoadScene("Registration");
    }

    //Go from the sign up page to the chess board
    public void SignUpButton2()
    {
        SceneManager.LoadScene("ChessBoard");
    }


}
