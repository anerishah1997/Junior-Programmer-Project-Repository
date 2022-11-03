using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR // we are adding conditional conditioning because, if we don't then it will throw error when this application is built & run on different platform.
    using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        MainManager.Instance.teamColor = color; // storing the selected color in our static instance variable, which is to be passed onto the Main Scene.
    }

   

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        ColorPicker.SelectColor(MainManager.Instance.teamColor); // This line will pre-select the saved color in the MainManager (if there is one) when the menu screen is launched.
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        // when exit is called, we will save the selected color.
        MainManager.Instance.SaveColor();

        // here we have to stop the playMode when this button is clicked. For that we have to give compiler the instruction that if this appln is running in Unity Editor then 
        //execute this code otherwise do Application.quit(). This concept is called 'CONDITIONAL COMPILING'.
        #if  UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void SaveColorBtnClicked()
    {
        // we will save the color selected when the button is clicked, so that we can load the saved color when the user clicks load button.
        MainManager.Instance.SaveColor();
    }

    public void LoadColorBtnClicked()
    {
        MainManager.Instance.LoadColor();
        // after loading the color in teamColor variable of MainManager class, we need to select that color.
        ColorPicker.SelectColor(MainManager.Instance.teamColor);
    }
}
