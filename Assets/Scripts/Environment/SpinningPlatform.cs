using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    private Transform platform;
    private float spinSpeed = 3f;


    private void Start()
    {
        platform = gameObject.transform;

        if (platform.position.x < 0) { spinSpeed *= -1; } 
    }


    private void FixedUpdate()
    {
        platform.Rotate(0, 0, spinSpeed);
    }
}
