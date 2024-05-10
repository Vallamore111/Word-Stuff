using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "Level", fileName = "New Level")]
public class Level : ScriptableObject
{
    [Header("Level Info")]
    public int levelNumber;
    public float timeLimitInSeconds;
    public GameObject levelLayout;
    public WordList wordList;
}