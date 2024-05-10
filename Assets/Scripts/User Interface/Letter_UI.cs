using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter_UI : MonoBehaviour
{
    public string assignedLetter;
    public bool letterCompleted;
    public RectTransform letterRect;
    public Sprite bubble;
    public TextMesh letterText;
    private Image letterImage;


    private void Awake()
    {
        letterImage = GetComponent<Image>();
    }
    
    private void Start()
    {
        letterText.text = assignedLetter;
        letterText.color = ColorPalette.fadedBlack;
    }


    public void CompleteLetter()
    {
        letterCompleted = true;
        letterImage.sprite = bubble;

        var parentWord = GetComponentInParent<Word_UI>();
        letterText.color = parentWord.assignedColor;
        parentWord.CheckIfComplete();
    }
}
