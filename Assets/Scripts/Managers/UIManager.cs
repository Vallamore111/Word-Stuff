using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject background;
    public GameObject spikes;
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject playerBasket;
    public GameObject wordList;
    public GameObject endLevelPopup;
    public GameObject textPopup;

    private void Awake()
    {
        instance = this;
    }


    public void Start()
    {
        levelMenu.SetActive(false);
        playerBasket.SetActive(false);
        wordList.SetActive(false);
        endLevelPopup.SetActive(false);
        mainMenu.SetActive(true);
    }


    public void ClickStartButton()
    {
        mainMenu.SetActive(false);
        spikes.SetActive(false);
        levelMenu.SetActive(true);
    }


    public void StartLevel()
    {
        levelMenu.SetActive(false);    
        background.SetActive(false);
        spikes.SetActive(false);
        endLevelPopup.SetActive(false);
        playerBasket.SetActive(true);
        wordList.SetActive(true);
    }


    public void EndLevel()
    {
        playerBasket.SetActive(false);
        wordList.SetActive(false);
        background.SetActive(true);
        spikes.SetActive(true);
        endLevelPopup.SetActive(true);
        endLevelPopup.GetComponent<Popup>().SetPopupWindow();
    }


    public void ShowLevelSelection()
    {
        endLevelPopup.SetActive(false);
        wordList.SetActive(false);
        spikes.SetActive(false);
        levelMenu.SetActive(true);
        levelMenu.GetComponentInChildren<LevelSelection>().ResetLevelSelectionMenu();
    }


    public void CreateTextPopup(int score)
    {
        var popup = Instantiate(textPopup);
        popup.GetComponent<TextPopup>().CreateTextPopup(score);
    }
}
