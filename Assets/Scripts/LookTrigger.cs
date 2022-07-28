using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class LookTrigger : MonoBehaviour
{
    public bool useAngleThreshold = false;
    [Range(0f, 90f)]
    public float angleThresholdDegrees = 30f;
    public float preciseness = 0.5f;
    public Transform playerObjTransform;
    public Color activatedColor = Color.green;
    public Color deactivatedColor = Color.red;

    private void OnDrawGizmos()
    {
        Vector2 lookTriggerCenter = transform.position;
        Vector2 playerPos = playerObjTransform.position;
        Vector2 playerLookDir = playerObjTransform.right; // x axis: works with 2D

        Vector2 playerToTriggerDir = (lookTriggerCenter - playerPos).normalized;

        // Get angle between 2 vectors. The dot product is a scalar projection:
        // project the look vector perpendicularly onto the direction from the player to the trigger, use the result for the preciseness threshold
        // When checking the preciseness value: it sets how far along this value should be for it to count as "looking at the thing" 
        float lookCloseness = Vector2.Dot(playerToTriggerDir, playerLookDir); // Project playerLookDir onto playerToTriggerDir
        lookCloseness = Mathf.Clamp(lookCloseness, -1, 1); // Prevent floating point shenanigans for safe normalization 
        float angleInRadians = Mathf.Acos(lookCloseness);
        float angleThresholdRadians = angleThresholdDegrees * Mathf.Deg2Rad;

        bool isLooking = false;
        
        if (useAngleThreshold == true) {
            isLooking = angleInRadians < angleThresholdRadians;
        }
        else
        {
            isLooking = lookCloseness >= preciseness; 
        }

        Gizmos.color = isLooking ? activatedColor : deactivatedColor;
        Gizmos.DrawLine(playerPos, playerPos + playerToTriggerDir);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerPos, playerPos + playerLookDir);
    }
}
