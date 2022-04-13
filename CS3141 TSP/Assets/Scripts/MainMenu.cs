using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MainMenu : MonoBehaviour
{

    private string input;
    public TMP_InputField username;
    public TMP_InputField password;


    void Security()
    {
      /*  string path = Application.dataPath + "/Log-in.txt";
        Debug.Log(path);
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Log-in \n\n");
        }
        string test = "test\n";
        File.AppendAllText(path, test);
       */    
     }

    void Start()
    {
        Security();

    }

    //Go from main menu to log in page
    public void LogInButton(){
        SceneManager.LoadScene("Log In");
    }

    //Go from login page to chess board
    public void LogInButton2()
    {
        //Debug.Log("please " + username.text);


        SceneManager.LoadScene("PlayScreen");
    }

    //Go from main menu to the sign up page
    public void SignUpButton(){
        SceneManager.LoadScene("Registration");
    }

    //Go from the sign up page to the chess board
    public void SignUpButton2()
    {
        string path = Application.dataPath + "/Log-in.txt";
        Debug.Log(path);
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Log-in \n\n");
        }
        string userInfo = username.text + "    " + password.text + "\n";
        File.AppendAllText(path, userInfo);
        SceneManager.LoadScene("PlayScreen");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("ChessBoard");
    }

}
