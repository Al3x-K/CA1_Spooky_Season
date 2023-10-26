using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        pauseMenu.SetActive(false);
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
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused)
                ResumeGame();
            else
                PauseGame();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
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
        pauseMenu.SetActive(false);
        healthBar.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GameOver()
    {
        isGameOver=true;
        SceneManager.LoadScene("GameOver");
        
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinGame");
    }
}