using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bubble : MonoBehaviour
{
    public TextMesh bubbleText;
    public int timerPenalty;
    private int spikeLayer;
    public static int bubbleScore;


    private void Awake()
    {
        spikeLayer = 8;
        bubbleScore = 50;
        timerPenalty = -1;
    }


    private void Start()
    {
        gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, ScaleManager.instance.dropScale);
        bubbleText.color = ColorPalette.GetColorNoBW();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == spikeLayer)
        { Destroy(gameObject); }
    }


}
