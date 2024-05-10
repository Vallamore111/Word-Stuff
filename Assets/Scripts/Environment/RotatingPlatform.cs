using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    private Transform platform;
    private float nextSpin;
    private float spinSpeed = 3f;
    private float spinInterval = 3f;
    private float currentRotation;
    private float targetRotation;
    private float rotationAmount = 90f;
    private bool rotationComplete;



    private void Start()
    {
        platform = gameObject.transform;
        nextSpin = Time.time + spinInterval;
        GetCurrentRotation();
    }


    private void FixedUpdate()
    {
        if (!rotationComplete)
        { RotateNinety(); }
        else { GetCurrentRotation(); }
    }


    private void GetCurrentRotation()
    {
        if (Time.time > nextSpin)
        {
            currentRotation = platform.rotation.z;
            targetRotation = currentRotation + rotationAmount;
            rotationComplete = false;
        }
    }


    private void RotateNinety()
    {
        if (currentRotation < targetRotation)
        { 
            platform.Rotate(0, 0, spinSpeed);
            currentRotation += spinSpeed;
        }
        else 
        {
            currentRotation = targetRotation;
            rotationComplete = true;
            nextSpin = Time.time + spinInterval;
        }
    }
}
