using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

public class MainMenu : MonoBehaviour
{

    private string input;
    public TMP_InputField username;
    public TMP_InputField password;

    //Go from main menu to log in page
    public void LogInButton(){
        SceneManager.LoadScene("Log In");
    }

    //Go from login page to chess board
    public void LogInButton2()
    {
        string path = Application.dataPath + "/Log-in.txt";
        Debug.Log(path);
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Log-in \n\n");
        }
        string[] vals = File.ReadAllLines(path);
        int exists = Array.IndexOf(vals, username.text);

        if( vals[exists + 1].Equals(Make256(password.text)) )
        {
            SceneManager.LoadScene("PlayScreen");
        }
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
        string userInfo = username.text + "\n" + Make256(password.text) + "\n";
        string[] vals = File.ReadAllLines(path);
        int exists = Array.IndexOf(vals, username.text);

        if (!(exists > 0))
        {
            File.AppendAllText(path, userInfo);
            SceneManager.LoadScene("PlayScreen");
        }
    }

    //Loads the actual game upon finishing loging in / signing up.
    public void PlayButton()
    {
        SceneManager.LoadScene("ChessBoard");
    }


    //Method for Encrypting user passwords.
    public string Make256(string text)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

            StringBuilder build = new StringBuilder();
            for(int i = 0; i < bytes.Length; i++)
            {
                build.Append(bytes[i].ToString("x2"));
            }

            return build.ToString();
        }
    }

}
