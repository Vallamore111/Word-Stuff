using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordListMenu : MonoBehaviour
{
    public GameObject wordPrefab;
    public GameObject wordList_UI;
    public List<Letter_UI> activeLetters;
    private List<Word_UI> wordsInUI;
    private LevelManager levelManager;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        wordsInUI = new List<Word_UI>();
        activeLetters = new List<Letter_UI>();
    }


    public void SetMenuToLevel()
    {
        ResetWordListMenu();

        for (int i = 0; i < WordManager.instance.wordsInLevel.Count; i++)
        {
            var word = Instantiate(wordPrefab, wordList_UI.transform);
            var wordInList = word.GetComponent<Word_UI>();
            wordsInUI.Add(wordInList);
            wordInList.assignedWord = WordManager.instance.wordsInLevel[i];
            wordInList.LoadWordUI();
        }
        SetWordSizes();
        SetActiveWord();
    }


    public void SetWordSizes()
    {
        ScaleManager.instance.SetWordListReductionPercentage();

        foreach (var word in wordsInUI)
        { ScaleManager.instance.SetWordSize(word); }
    }


    public void SetActiveWord()
    {
        for (int i = 0; i < wordsInUI.Count; i++)
        {
            if (!wordsInUI[i].wordCompleted)
            {
                SetActiveLetters(wordsInUI[i]);
                SpawnManager.instance.SetDropList();
                return;
            }
        }
        levelManager.levelCompleted = true;
        StartCoroutine(levelManager.EndLevel());

    }


    private void SetActiveLetters(Word_UI word)
    {
        activeLetters.Clear();

        for (int i = 0; i < word.lettersInWord.Count; i++)
        { 
            activeLetters.Add(word.lettersInWord[i]); 
        }
    }


    public void ResetWordListMenu()
    {
        activeLetters = new List<Letter_UI>();
        wordsInUI = new List<Word_UI>();

        foreach (Transform child in wordList_UI.transform)
        { Destroy(child.gameObject); }
    }
}
