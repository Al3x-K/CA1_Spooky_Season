using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

//the base of the script is the code taken from Naoise's class 
//CHANGES MADE:
//1. added pause menu and health bar that disappears when pause is active
//2. added respective scenes for various functions (eg. GameOver scene to GameOver function) 
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Game States")]
    public bool isPaused;
    public bool isGameOver;
    public GameObject pauseMenu;
    public GameObject healthBar;
    
       void Awake()
    {
        pauseMenu.SetActive(false); //sets the pause menu to false so it's not visible at the start of the game
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; 
        } 
        
    }

    
    void Update()
    {
        //pause the game on P
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused)
                ResumeGame();
            else
                PauseGame();
        }
        //Restarts the game on R
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        //Quits the game on Escape
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
        

    }   
    
    
    public void PauseGame()
    {
        pauseMenu.SetActive(true); //it activates the panel with pause menu
        healthBar.SetActive(false);//hides the hearts
        Time.timeScale = 0f; //freezes the time
        isPaused = true; //sets the boolean to true cuz the game is paused
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);//it closes the panel with pause menu
        healthBar.SetActive(true);//shows the hearts
        Time.timeScale = 1f;//unfreezes the time
        isPaused = false;//sets the boolean to false cuz the game isn't paused anymore
    }

    //loads the game over scene
    public void GameOver()
    {
        isGameOver=true;
        SceneManager.LoadScene("GameOver");
        
    }

    //loads the current scene, which is the level, again 
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //loads main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    //loads won game scene
    public void WinGame()
    {
        SceneManager.LoadScene("WinGame");
    }
}