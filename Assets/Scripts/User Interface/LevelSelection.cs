using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public GameObject levelButton;
    private int levelCount;


    private void Awake()
    {
        levelCount = 15;
    }

    public void SetLevelSelectionMenu()
    {
        for (int i = 0; i < levelCount; i++)
        {
            GameObject level = Instantiate(levelButton, gameObject.transform);
            var levelInfo = level.GetComponent<LevelUIButton>();
            LevelUIButton.levelCounter++;
            levelInfo.SetLevelInfo();
        }
    }


    public void ResetLevelSelectionMenu()
    { 
        LevelUIButton.levelCounter = 0;
        SetLevelSelectionMenu();
    }
}
