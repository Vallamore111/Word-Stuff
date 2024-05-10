using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Word_UI : MonoBehaviour
{
    public List<GameObject> letterHoldersUI;
    public bool wordCompleted;
    public RectTransform wordRect;
    private WordListMenu wordListMenu;
    [System.NonSerialized] public string assignedWord;
    [System.NonSerialized] public Color assignedColor;
    public List<Letter_UI> lettersInWord;


    private void Awake()
    {
        wordListMenu = FindObjectOfType<WordListMenu>();
    }


    public void LoadWordUI()
    {
        assignedColor = ColorPalette.GetColorNoBW();

        for (int i = 0; i < assignedWord.Length; i++)
        {
            var letter = letterHoldersUI[i].GetComponent<Letter_UI>();
            letter.assignedLetter = assignedWord[i].ToString();
            lettersInWord.Add(letter);
        }
        DeleteUnusedLetters();
    }


    public void CheckIfComplete()
    {
        for (int i = 0; i < lettersInWord.Count; i++)
        {
            if (!lettersInWord[i].letterCompleted) return;
        }
        wordCompleted = true;
        wordListMenu.SetActiveWord();
    }


    private void DeleteUnusedLetters()
    {
        int unusedCount = letterHoldersUI.Count - assignedWord.Length;

        for (int i = assignedWord.Length; i < letterHoldersUI.Count; i++)
        { Destroy(letterHoldersUI[i]); }

        letterHoldersUI.RemoveRange(assignedWord.Length, unusedCount);
    }
}
