using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    public int starCount;
    public Sprite lockedStar;
    public Sprite unlockedStar;
    public Image[] stars;
    private LevelManager levelManager;
    private DataManager dataManager;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        dataManager = FindObjectOfType<DataManager>();
    }


    public void SetStarCount(float score, int[] milestones)
    {
        starCount = 0;
        if (!levelManager.levelCompleted) return;

        if (score >= milestones[2])
        { starCount = 3; }

        else if (score >= milestones[1])
        {
            if (starCount == 2) return;
            starCount = 2;
            levelManager.UnlockNewLevel();
        }

        else if (score >= milestones[0])
        { starCount = 1; }

        if (starCount > dataManager.levelData.starsAcquired)
        { dataManager.levelData.starsAcquired = starCount; }

        SetStars(stars, starCount);
    }


    public void SetStars(Image[] stars, int starCount)
    {
        if (starCount == 0)
        {
            stars[0].sprite = lockedStar;
            stars[1].sprite = lockedStar;
            stars[2].sprite = lockedStar;
        }

        if (starCount == 1)
        {
            stars[0].sprite = unlockedStar;
            stars[1].sprite = lockedStar;
            stars[2].sprite = lockedStar;
        }

        if (starCount == 2)
        {
            stars[0].sprite = unlockedStar;
            stars[1].sprite = unlockedStar;
            stars[2].sprite = lockedStar;
        }

        if (starCount == 3)
        {
            stars[0].sprite = unlockedStar;
            stars[1].sprite = unlockedStar;
            stars[2].sprite = unlockedStar;
        }
    }

}
