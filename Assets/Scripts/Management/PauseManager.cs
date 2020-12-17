using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject PausedPanel;
    public GameObject BoardPanel;
    public GameObject MenuButtonBackgroud;
    private GameObject board;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
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
        }
        else
        {
            board.SetActive(true);
            Time.timeScale = 1;
            isPaused = true;
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

        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            MenuButtonBackgroud.SetActive(false);
            BoardPanel.SetActive(false);
            PausedPanel.SetActive(true);
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
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
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

    }
}