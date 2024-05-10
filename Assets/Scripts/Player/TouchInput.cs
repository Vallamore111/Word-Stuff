using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour
{
    public Vector2 dropHeight;
    public Vector2 dropWidth;
    public Vector3 clickPosition;
    private float xClamp;
    private float yClamp;
    private float xWordListOffset;
    private float xBasketOffset;
    private float yBasketOffset;
    private Camera mainCamera;


    private void Awake()
    {
        mainCamera = Camera.main;
        xBasketOffset = 2.75f;
        yBasketOffset = 5f;

        GetBasketClamps();
        GetDropHeightClamps();
    }


    private void Update()
    {
        clickPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }


    public Vector3 SetBasketPosition()
    {
        Vector3 clampedPosition = new Vector3
        (
            Mathf.Clamp(clickPosition.x, -xClamp, xClamp - xWordListOffset),
            Mathf.Clamp(clickPosition.y, -yClamp, -yClamp),
            -1
        );
        return clampedPosition;
    }
    

    private void GetBasketClamps()
    {
        float orthoHeightInUnits = mainCamera.orthographicSize;
        float orthoWidthInUnits = orthoHeightInUnits * mainCamera.aspect;
        float totalUnitWidth = orthoWidthInUnits * 2;
        float wordListPercentageWidth = .25f;

        xClamp = orthoWidthInUnits - xBasketOffset;
        yClamp = orthoHeightInUnits - yBasketOffset;
        xWordListOffset = totalUnitWidth * wordListPercentageWidth;
    }


    private void GetDropHeightClamps()
    {
        float xOffset = 2f;
        float xWordListOffset = 19f;
        float yMinOffset = 2;
        float yMaxOffset = 12;
        float heightInUnits = mainCamera.orthographicSize;
        float widthInUnits = heightInUnits * mainCamera.aspect;

        dropHeight = new Vector2(heightInUnits + yMinOffset, heightInUnits + yMaxOffset);
        dropWidth = new Vector2(-widthInUnits + xOffset, widthInUnits - xOffset - xWordListOffset);
    }

}
