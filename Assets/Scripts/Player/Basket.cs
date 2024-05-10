using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Basket : MonoBehaviour
{
    //public List<string> bubblesInBasket;
    private Bubble bubbleCaught;
    //private Coin coinCaught;
    //private Bomb bombCaught;
    //private Stone stoneCaught;
    private TouchInput touchInput;
    private WordListMenu wordListMenu;
    //private HUD hudInterface;



    private void Awake()
    {
        touchInput = FindObjectOfType<TouchInput>();
        wordListMenu = FindObjectOfType<WordListMenu>();
        //bubblesInBasket = new List<string>();

        //hudInterface = FindObjectOfType<HUD>();
    }

    private void Start()
    {
        transform.position = touchInput.SetBasketPosition();
        transform.localScale = Vector3.Scale(transform.localScale, ScaleManager.instance.dropScale);
    }

    private void Update()
    {
        transform.position = touchInput.SetBasketPosition();
    }


    private void CheckLetterCaught(string letter)
    {
        int index = 0;

        for (int i = 0; i < wordListMenu.activeLetters.Count; i++)
        {
            if (!wordListMenu.activeLetters[i].letterCompleted)
            { index = i; break; };
        }

        if (letter == wordListMenu.activeLetters[index].assignedLetter)
        {
            wordListMenu.activeLetters[index].CompleteLetter();
            ScoreManager.instance.UpdateScore(Bubble.bubbleScore);
            ScoreManager.instance.IncreaseMultiplier();
        }
        else
        {
            ScoreManager.instance.ResetMultiplier();
            Timer.instance.PenalizeTime(bubbleCaught.timerPenalty);
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bubble>(out bubbleCaught))
        {
            //bubblesInBasket.Add(bubbleCaught.bubbleText.text);
            CheckLetterCaught(bubbleCaught.bubbleText.text);
            Destroy(bubbleCaught.gameObject);
            //hudInterface.UpdateBasket();
            return;
        }

        /*if (collision.gameObject.TryGetComponent<Coin>(out coinCaught))
        {
            coinCaught.CatchCoin();
            hudInterface.UpdateCoinCount();
            return;
        }

        if (collision.gameObject.TryGetComponent<Stone>(out stoneCaught))
        {
            stoneCaught.CatchStone();
            hudInterface.DisplayLoss(stoneCaught.lettersToDestroy);
            hudInterface.UpdateBasket();
            return;
        }

        if (collision.gameObject.TryGetComponent<Bomb>(out bombCaught))
        {
            bombCaught.CatchBomb();
            hudInterface.explosion.gameObject.SetActive(true);
            hudInterface.explosion.Play();
            hudInterface.UpdateBasket();
        }*/
    

    }

}
