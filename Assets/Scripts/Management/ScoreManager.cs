using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public static int score;
    public static int bonusMultiplier;

    void Start()
    {
        score = 0;
        bonusMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText($"{score}");
        CheckScore();
    }

    public static void IncrementScore(int scoreValue)
    {
        score += scoreValue * bonusMultiplier;
    }

    public static void IncrementMultiplier()
    {
        bonusMultiplier++;
    }
    public static void CheckScore()
    {
        if (score >= 50)
        {
            Debug.Log("You Win!!!");
        }
    }

    public static void ResetMultiplier()
    {
        bonusMultiplier = 1;
    }

}
