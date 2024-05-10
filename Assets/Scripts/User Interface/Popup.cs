using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Text level;
    public Text score;
    public Text levelMessage;
    public Button nextLevelButton;
    private StarManager starManager;



    private void Awake()
    {
        starManager = FindObjectOfType<StarManager>();
    }


    public void SetPopupWindow()
    {
        CheckForUnlockedLevel();
        GetLevelMessage(starManager.starCount);
        level.text = "Level " + LevelManager.currentLevel.levelNumber;
        score.text = "Score: " + ScoreManager.instance.GetScore().ToString();
    }


    public void SetLevelMessage(string text) => levelMessage.text = text;

    private void GetLevelMessage(int stars)
    {
        switch (stars)
        {
            case 3:
                SetLevelMessage("LEVEL COMPLETE!");
                break;

            case 2:
                SetLevelMessage("NEW LEVEL UNLOCKED!");
                break;

            default:
                SetLevelMessage("TRY AGAIN!");
                break;
        }
    }


    private void CheckForUnlockedLevel()
    {
        if (starManager.starCount >= 2)
        { nextLevelButton.interactable = true; return; }
        else { nextLevelButton.interactable = false; }
    }
}
