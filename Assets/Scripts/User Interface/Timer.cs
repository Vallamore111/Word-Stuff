using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeBonus;
    public bool timeExpired;
    private bool timerRunning;
    private float timeRemaining;
    private Text timerText;
    private LevelManager levelManager;
    public static Timer instance;



    private void Awake()
    {
        instance = this;
        timerText = GetComponent<Text>();
        levelManager = FindObjectOfType<LevelManager>();
    }


    public void StartTimer()
    {
        float timeDisplayBuffer = 1f;
        timeRemaining = LevelManager.currentLevel.timeLimitInSeconds;
        timeRemaining += timeDisplayBuffer;
        timerRunning = true;
        timeExpired = false;
    }


    private void Update()
    {
        if (!timerRunning) return;
        HandleCountdown();
    }


    public int GetTimeRemaining()
    { 
        int timeLeft = Mathf.RoundToInt(timeRemaining);
        return timeLeft;
    }


    private void HandleCountdown()
    {
        if (timeRemaining > Time.deltaTime)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeDisplay(timeRemaining);
        }
        else
        {
            timerRunning = false;
            timeRemaining = 0;
            UpdateTimeDisplay(timeRemaining);
            StartCoroutine(levelManager.EndLevel());
        }
    }


    public void UpdateTimeDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }


    public void PenalizeTime(int penalty)
    {
        timeRemaining += penalty;
        UpdateTimeDisplay(timeRemaining);
        ShowTimerPopup(penalty);
    }


    public void ShowTimerPopup(int time)
    {
        if (time < 0)
        {
            timeBonus.text = time.ToString();
            timeBonus.color = Color.red;
        }
        else 
        {
            timeBonus.text = "+" + time.ToString();
            timeBonus.color = Color.green;
        }
        timeBonus.gameObject.SetActive(true);
        StartCoroutine(ColorPalette.FadeAlpha(timeBonus));

    }


    public void GetTimeBonus()
    {
        int timeLeft = Mathf.RoundToInt(timeRemaining);
        int timeBonus = ScoreManager.instance.SetTimeBonus(timeLeft);

        if (timeLeft == 0)
        {
            timeExpired = true;
            return;
        }
        ShowTimerPopup(timeBonus);
    }
}
