using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ScoreManager.IncrementScore(1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ScoreManager.IncrementMultiplier();
        }
    }
}
