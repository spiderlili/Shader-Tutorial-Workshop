using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigonometry : MonoBehaviour
{
    private const float TAU = 6.28318530718f; // Full turn in radians
    public int dotCount = 16;
    public float dotRadius = 0.06f;
    
    private void RadiansToDegrees()
    {
        float radians = Mathf.PI * 2f;
        float degrees = radians * Mathf.Rad2Deg; // 360
    }

    private void OnDrawGizmos()
    {
        // Calculate an angle: divide full turn (TAU) into the angle to each dot
        // For each of the iterations: need the angle to each dot, skip the start & end point using dotCount -1
        for (int i = 0; i < dotCount; i++) {
            float angleInTurns = i / ((float)dotCount);
            float angRad = angleInTurns * TAU; // use this to get the direction vector
            float x = Mathf.Cos(angRad);
            float y = Mathf.Sin(angRad);
            Vector2 point = new Vector2(x, y);
            Gizmos.DrawSphere(point, dotRadius);
        }
    }

    private void Start()
    {

    }
}
