using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string chosenLevel;
    public bool levelCompleted;
    public static Level currentLevel;
    public List<Level> allLevels = new List<Level>();

    private GameObject levelLoaded;
    private ScaleManager scaleManager;
    private DataManager dataManager;


    private void Awake()
    {
        scaleManager = FindObjectOfType<ScaleManager>();
        dataManager = FindObjectOfType<DataManager>();
    }


    private void InitializeLevel()
    {
        levelCompleted = false;
        WordManager.instance.RetrieveWords();
        UIManager.instance.StartLevel();
        scaleManager.SetLevelToScreenSize(levelLoaded);
        SpawnManager.instance.EnableSpawns();
        Timer.instance.StartTimer();
        ScoreManager.instance.ResetScore();
        ScoreManager.instance.ResetMultiplier();
    }


    public void LoadLevel()
    {
        int levelToLoad = int.Parse(chosenLevel);
        levelToLoad--;
        currentLevel = allLevels[levelToLoad];
        levelLoaded = Instantiate(currentLevel.levelLayout);
        dataManager.LoadLevelData(currentLevel);

        InitializeLevel();
    }


    public void LoadNextLevel()
    {
        int nextLevel = currentLevel.levelNumber;
        nextLevel++;
        chosenLevel = nextLevel.ToString();
        LoadLevel();
    }


    public void UnlockNewLevel()
    {
        int newLevelIndex = currentLevel.levelNumber;
        newLevelIndex++;

        foreach (var level in dataManager.gameData.levels)
        {
            if (level.levelNumber == newLevelIndex)
                return;
        }

        LevelData newLevel = new LevelData();
        newLevel.levelNumber = newLevelIndex;
        dataManager.SaveLevelData(newLevel);
    }


    public IEnumerator EndLevel()
    {
        SpawnManager.instance.DisableSpawns();
        Timer.instance.GetTimeBonus();
        yield return new WaitForSeconds(2);
        ScoreManager.instance.SetFinalScore();
        UIManager.instance.EndLevel();
        Destroy(levelLoaded);
    }
}
