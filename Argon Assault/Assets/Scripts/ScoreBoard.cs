using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    // cached parameters
    TextMeshProUGUI scoreText;

    // state parameters
    int score = 0;

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void AddToScore(int amount)
    {
        score += amount;
        UpdateScore();
    }
}
