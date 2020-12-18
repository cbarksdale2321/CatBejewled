using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameType
{
    Moves,
    Time
}


public class EndGameManager : MonoBehaviour
{

    public TimeManager timeManager;
    [SerializeField] private Text movesLeft;
    public static int moves;
    // Start is called before the first frame update
    void Start()
    {
        moves = 5;
        movesLeft.text = string.Format("<color=white>Moves Left: {0}</color>", moves);
    }

    // Update is called once per frame
    void Update()
    {
        TimeManager.CheckTimeZero();

        

    }

    private void CheckMoves()
    {
        if (moves <= 0)
        {
            //Game over stuff here
            Debug.Log("Game Over!");
        }
    }
}
