using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //It loads the scene that has index 1 in the build settings, so the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Opens the scene with settings
    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    //goes back to the main menu scene
    public void CloseSettings()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        //quits the whole thing
        Application.Quit();
    }
    
}
