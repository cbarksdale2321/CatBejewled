using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    //public Text timeText;
    public TextMeshProUGUI timeText;
    public static float timer;
    void Start()
    {
        DisplayTime(0);
        timer = 60;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(timer -= Time.deltaTime);
        //checkTimeZero();
    }

     void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));

        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public static void  CheckTimeZero()
    {
        if (timer <= 1 )
        {
            if (ScoreManager.score >=50 )
            {
                FindObjectOfType<AudioManager>().Play("CatWin");
            }
            //SceneManager.LoadScene(2);
            //Instead of opening scene, reopen pause menu with restart text instead
            //
            FindObjectOfType<PauseManager>().GameOver();
        }
    }

}
