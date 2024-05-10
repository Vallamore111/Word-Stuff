using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    public int letterCount;
    public int longestWord;
    public List<string> wordsInLevel;
    private WordListMenu wordListMenu;
    [System.NonSerialized] public WordList wordList;


    private void Awake()
    {
        instance = this;
        wordListMenu = FindObjectOfType<WordListMenu>();
        wordsInLevel = new List<string>();
    }


    public void RetrieveWords()
    {
        wordList = LevelManager.currentLevel.wordList;

        if (wordList == null) return;
        wordsInLevel.Clear();

        if (wordList.entry_1 != string.Empty) { wordsInLevel.Add(wordList.entry_1); }
        if (wordList.entry_2 != string.Empty) { wordsInLevel.Add(wordList.entry_2); }
        if (wordList.entry_3 != string.Empty) { wordsInLevel.Add(wordList.entry_3); }
        if (wordList.entry_4 != string.Empty) { wordsInLevel.Add(wordList.entry_4); }
        if (wordList.entry_5 != string.Empty) { wordsInLevel.Add(wordList.entry_5); }
        if (wordList.entry_6 != string.Empty) { wordsInLevel.Add(wordList.entry_6); }
        if (wordList.entry_7 != string.Empty) { wordsInLevel.Add(wordList.entry_7); }
        if (wordList.entry_8 != string.Empty) { wordsInLevel.Add(wordList.entry_8); }
        if (wordList.entry_9 != string.Empty) { wordsInLevel.Add(wordList.entry_9); }
        if (wordList.entry_10 != string.Empty) { wordsInLevel.Add(wordList.entry_10); }

        RetrieveLetterCount(wordsInLevel);
        longestWord = GetLongestWord();
        wordListMenu.SetMenuToLevel();
    }


    public void RetrieveLetterCount(List<string> words)
    {
        letterCount = 0;

        foreach (var word in words)
        { letterCount += word.Length; }
    }


    public int GetLongestWord()
    {
        int longestWord = 0;

        for (int i = 0; i < wordsInLevel.Count; i++)
        {
            if (wordsInLevel[i].Length > longestWord)
            { longestWord = wordsInLevel[i].Length; }
        }
        return longestWord;
    }

}

