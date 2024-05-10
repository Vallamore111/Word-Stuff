using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleManager : MonoBehaviour
{
    public static ScaleManager instance;

    public Vector3 dropScale;
    private Camera mainCamera;
    private float wordListReductionPercentage;


    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
    }

    private void Start()
    {
        dropScale = Vector3.one;
    }


    public void SetLevelToScreenSize(GameObject level)
    {
        float targetAspect = 2960f / 1440f;
        float safeAspect = 16f / 9f;
        var objects = level.GetComponent<ContentScaler>();

        if (mainCamera.aspect < safeAspect)
        {
            float newScale = mainCamera.aspect / targetAspect;
            dropScale = new Vector3(newScale, newScale, 1);
            objects.objectsToScale.transform.localScale = dropScale;
        }
        else dropScale = Vector3.one;
    }


    public void SetWordListReductionPercentage()
    {
        float[] preferredLetterSizes = new float[5];
        preferredLetterSizes[0] = 115f;
        preferredLetterSizes[1] = 94.5f;
        preferredLetterSizes[2] = 77.5f;
        preferredLetterSizes[3] = 67.5f;
        preferredLetterSizes[4] = 58.5f;
        
        int longestWord = WordManager.instance.longestWord;
        int safeLength = 4;
        int difference = longestWord - safeLength;
        wordListReductionPercentage = preferredLetterSizes[difference] / preferredLetterSizes[0];
    }


    public void SetWordSize(Word_UI word)
    {
        float padding = 1.2f;
        float newRectHeight = word.wordRect.rect.size.y * wordListReductionPercentage / padding;
        Vector2 newWordSize = new Vector2(word.wordRect.rect.size.x, newRectHeight);

        word.wordRect.sizeDelta = newWordSize;
        SetLetterSizes(word);
    }


    private void SetLetterSizes(Word_UI word)
    {
        foreach (var letter in word.lettersInWord)
        {
            float newRectWidth = letter.letterRect.rect.size.x * wordListReductionPercentage;
            float newFontSize = letter.letterText.fontSize * wordListReductionPercentage;
            
            Vector2 newletterSize = new Vector2(newRectWidth, letter.letterRect.rect.size.y);
            letter.letterRect.sizeDelta = newletterSize;

            letter.letterText.fontSize = (int)newFontSize;
        }
    }



}
