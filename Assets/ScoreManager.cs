using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private static int score;

    void Start()
    {
        scoreText.SetText($"Score = {score}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
