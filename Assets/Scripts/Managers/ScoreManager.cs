using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public bool scoreBarAvailable;
    public Text scoreText;
    public Text multiplierText;
    public Slider scoreBar;
    public GameObject textPopup;
    private int currentScore;
    private int currentMultiplier;
    private int[] scoreThresholds;
    private StarManager starManager;


    private void Awake()
    {
        instance = this;
        scoreThresholds = new int[3];
        starManager = FindObjectOfType<StarManager>();
    }


    private void Update()
    {
        FillScoreBar();
    }


    public int GetScore()
    {
        int score = currentScore;
        return score;
    }


    public void ResetScore()
    {
        currentScore = 0;
        scoreBar.value = 0;
        UpdateScore(currentScore);
    }


    public void UpdateScore(int points)
    {
        int totalPoints = points * currentMultiplier;
        currentScore += totalPoints;
        scoreText.text = currentScore.ToString();

        if (currentScore == 0) return;
        UIManager.instance.CreateTextPopup(totalPoints);
    }


    public void SetFinalScore()
    {
        SetScoreThresholds();
        starManager.SetStarCount(currentScore, scoreThresholds);
        SetScoreBarValues();
        SaveManager.instance.SaveProgress();
    }


    public void SetScoreThresholds()
    {
        int baseScore = Bubble.bubbleScore;
        int totalLetters = WordManager.instance.letterCount;
        int maxMultiplier = totalLetters + 1;
        int maxScore = 0;
        float secondThresholdPercent = 0.65f;

        for (int i = 0; i < maxMultiplier; i++)
        { maxScore += i * baseScore; }

        scoreThresholds[0] = baseScore * totalLetters;
        scoreThresholds[1] = (int)(maxScore * secondThresholdPercent);
        scoreThresholds[2] = maxScore;

    }


    private void SetScoreBarValues()
    {
        scoreBar.maxValue = scoreThresholds[2];

        if (Timer.instance.timeExpired)
        { 
            scoreBar.minValue = 0;
            scoreBar.value = 0;
            starManager.SetStars(starManager.stars, 0);
        }
        else
        {
            scoreBar.minValue = scoreThresholds[0];
            scoreBarAvailable = true;
        }
    }


    public void FillScoreBar()
    {
        if (!scoreBarAvailable) return; 

        int score = GetScore();
        int lerpThreshold = 10;
        float dampener = 1.5f;

        scoreBar.value = Mathf.Lerp(scoreBar.value, score, Time.deltaTime / dampener);
        starManager.SetStarCount(scoreBar.value, scoreThresholds);

        if (scoreBar.value > score - lerpThreshold)
        {
            scoreBar.value = score;
            scoreBarAvailable = false; 
        }
    }


    public void IncreaseMultiplier()
    { 
        currentMultiplier++;
        multiplierText.text = currentMultiplier.ToString() + "x";
    }


    public void ResetMultiplier()
    {
        currentMultiplier = 1;
        multiplierText.text = currentMultiplier.ToString() + "x";
    }


    public int SetTimeBonus(int time)
    {
        int timePoints = 100;
        int timeBonus = time * timePoints;
        currentScore += timeBonus;
        return timeBonus;
    }   
    


}
