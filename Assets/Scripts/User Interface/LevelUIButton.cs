using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LevelUIButton : MonoBehaviour, IPointerDownHandler
{
    public bool levelUnlocked;
    public Text levelNumber;
    public Image levelLock;
    public Image[] stars;
    private int savedStars;
    private Image levelIcon;
    private LevelManager levelManager;
    private DataManager dataManager;
    private StarManager starManager;
    public static int levelCounter;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        dataManager = FindObjectOfType<DataManager>();
        starManager = FindObjectOfType<StarManager>();
        levelIcon = GetComponent<Image>();
    }

    public void SetLevelInfo()
    {
        GetLevelInfo();
        starManager.SetStars(stars, savedStars);

        if (levelCounter == 1) 
        { levelUnlocked = true; }

        if (levelUnlocked)
        {
            levelIcon.raycastTarget = true;
            levelIcon.color = Color.white;
            levelLock.color = Color.clear;
            levelNumber.color = Color.black;
        }
        else
        {
            levelIcon.raycastTarget = false;
            levelIcon.color = ColorPalette.fadedBlack;
            levelLock.color = Color.white;
            levelNumber.color = Color.clear;
        }
    }


    private void GetLevelInfo()
    {
        levelNumber.text = levelCounter.ToString();

        if (dataManager.gameData == null) return;

        int level = int.Parse(levelNumber.text);
        var gameData = dataManager.gameData.levels;

        for (int i = 0; i < gameData.Count; i++)
        {
            if (level == gameData[i].levelNumber)
            {
                levelUnlocked = true;
                savedStars = gameData[i].starsAcquired;
                break;
            }
        }
    }

    private void OnDisable() => Destroy(gameObject);


    public void OnPointerDown(PointerEventData eventData)
        {
            levelManager.chosenLevel = levelNumber.text;
            levelManager.LoadLevel();
        }
}
