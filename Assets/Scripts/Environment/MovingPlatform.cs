using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private TouchInput touchInput;
    private Transform platform;
    private float moveSpeed = 10f;
    private float platformPadding = 2f;
    public bool movingRight;
    public bool movingLeft;


    private void Start()
    {
        touchInput = FindObjectOfType<TouchInput>();
        platform = gameObject.transform;
        movingRight = true;
        movingLeft = false;
    }


    void FixedUpdate()
    {
        if (movingRight)
        { platform.position += platform.right * moveSpeed * Time.deltaTime; }


        if (movingLeft)
        { platform.position -= platform.right * moveSpeed * Time.deltaTime; }


        if (movingRight && platform.position.x >= touchInput.dropWidth.y - platformPadding)
        {
            movingRight = false;
            movingLeft = true;
        }


        if (movingLeft && platform.position.x <= touchInput.dropWidth.x + platformPadding)
        {
            movingLeft = false;
            movingRight = true;
        }


        

    }


}
