﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private static int score;
    private static int bonusMultiplier;

    void Start()
    {
        score = 0;
        bonusMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText($"Score = {score}");
    }

    public static void IncrementScore(int scoreValue)
    {
        score += scoreValue * bonusMultiplier;
    }

    public static void IncrementMultiplier()
    {
        bonusMultiplier++;
    }

}