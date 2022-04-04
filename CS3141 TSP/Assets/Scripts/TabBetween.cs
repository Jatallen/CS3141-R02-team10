using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabBetween : MonoBehaviour
{   
    //Input field values to switch between
    public TMP_InputField username;//0
    public TMP_InputField password;//1

    public int InputSelected;

    //Each time a tab or shift tab is pressed change input field
    private void Update()
    {
        //Go up if shift and tab are pressed
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            InputSelected--;
            if (InputSelected < 0)
            {
                InputSelected = 1;
            }
            SelectInputField();
        }//Go down if shift is pressed
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (InputSelected > 1)
            {
                InputSelected = 0;
            }
            SelectInputField();
        }
        //Check which input field is selected
        void SelectInputField()
        {
            switch (InputSelected)
            {
                case 0: username.Select();
                    break;
                case 1: password.Select();
                    break;

            }
        }
    }

    public void usernameSelected() => InputSelected = 0;
    public void passwordSelected() => InputSelected = 1;


}
