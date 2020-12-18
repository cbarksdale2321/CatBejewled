using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject PausedPanel;
    public GameObject BoardPanel;
    public GameObject MenuButtonBackgroud;
    public GameObject Timed;
    private GameObject board;
    public bool isPaused = false;


    public GameObject GameOverText;
    public GameObject PauseText;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
        Timed.SetActive(true);
        board = GameObject.FindGameObjectWithTag("Board");
    }

    public void QuitGame()
    {
        //Closes Application
        Application.Quit();
    }
    public void PauseGame()
    {
        //Pauses Game when the button is hit. 
        //Also stops the time from the timer. 
        if (isPaused)
        {
            
            Time.timeScale = 0;
            board.SetActive(false);
            isPaused = false;
            BoardPanel.SetActive(true);
            MenuButtonBackgroud.SetActive(true);
            Timed.SetActive(true);
        }
        else
        {
            board.SetActive(true);
            Time.timeScale = 1;
            isPaused = true;
            Timed.SetActive(false);
            MenuButtonBackgroud.SetActive(false);
            BoardPanel.SetActive(false);
            PausedPanel.SetActive(true);
        }
    }
    public void UnPauseGame()
    {
        //IF TAKEN OUT... Button does not work properly
        //When hit in Pause Menu, starts to countdown time and game goes back to normal.
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            BoardPanel.SetActive(true);
            PausedPanel.SetActive(false);
            MenuButtonBackgroud.SetActive(true);
            Timed.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            MenuButtonBackgroud.SetActive(false);
            BoardPanel.SetActive(false);
            //PausedPanel.SetActive(true);
            Timed.SetActive(false);
        }
    }
    public void ReloadScene()
    {
        //Reloads the game. 
        SceneManager.LoadScene("djscene", LoadSceneMode.Single);
        //Fixes Issue with Timer.
        //Reloads Timer and then counts down when the game is restarted
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            PausedPanel.SetActive(false);
            Timed.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            Timed.SetActive(false);
            PausedPanel.SetActive(true);
        }
    }
    public void ReturnMenu()
    {
        //Code I have put does not work... :(
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                UnPauseGame();
            else
                PauseGame();
        }
    }

    public void GameOver()
    {
        board.SetActive(true);
        Time.timeScale = 1;
        isPaused = true;
        Timed.SetActive(false);
        MenuButtonBackgroud.SetActive(false);
        BoardPanel.SetActive(false);
        PausedPanel.SetActive(true);
        GameOverText.SetActive(true);
        PauseText.SetActive(false);

    }
}