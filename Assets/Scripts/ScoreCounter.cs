using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private static ScoreCounter instance;

    [SerializeField] TMP_Text scoreText;
    private int score;

    public static ScoreCounter Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseScore()
    {
        score++;
    }
}
