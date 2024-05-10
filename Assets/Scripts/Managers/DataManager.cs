using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameData gameData;
    public LevelData levelData;


    public void SaveLevelData(LevelData data)
    {
        if (gameData.levels.Count == 0)
        {
            gameData.levels.Add(data); 
            return; 
        }


        for (int i = 0; i < gameData.levels.Count; i++)
        {
            if (gameData.levels[i].levelNumber == data.levelNumber)
            {
                gameData.levels.RemoveAt(i);
                gameData.levels.Add(data);
                return;
            }
        }
        gameData.levels.Add(data);
    }



    public void LoadLevelData(Level level)
    {
        if (gameData.levels.Count == 0)
        {
            levelData.levelNumber = level.levelNumber; 
            return; 
        }

        for (int i = 0; i < gameData.levels.Count; i++)
        {
            if (gameData.levels[i].levelNumber == level.levelNumber)
            {
                levelData = gameData.levels[i];
                return;
            }
        }
    }
}
