using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class LookTrigger : MonoBehaviour
{
    public bool useAngleThreshold = false;
    [Range(0f, 90f)]
    public float angleThresholdDegrees = 30f;
    public float preciseness = 0.5f;
    public Transform playerObjTransform;
    public Color activatedColor = Color.green;
    public Color deactivatedColor = Color.red;
    public int lines = 128;
    
    private const float TAU = 6.28318530718f; // Full turn in radians
    public bool visualiseAnglesInLookRange = true;
    public bool visualiseAngleThreshold = false;
    Vector2 AngleToDirection(float angleInRadians)
    {
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }

    float DirectionToAngle(Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x);
    }

    // Draw a fan of lines to visualize the angles more clearly, show whether those are within / outside of the look range
    private void OnDrawGizmos()
    {
        Vector2 lookTriggerCenter = transform.position;
        Vector2 playerPos = playerObjTransform.position;
        Vector2 playerLookDir = playerObjTransform.right; // x axis: works with 2D
        Vector2 playerToTriggerDir = (lookTriggerCenter - playerPos).normalized;
        
        if (visualiseAnglesInLookRange) {
            DrawLookLine(playerObjTransform.transform.right);
            for (int i = 0; i < lines; i++) {
                float turns = i / (float)lines;
                float angleInRadians = turns * TAU;
                Vector2 dir = AngleToDirection(angleInRadians);
                DrawLookLine(dir);
            }
            VisualizeAngleThreshold(playerPos, playerToTriggerDir);
        }
        else if (visualiseAngleThreshold) {
            VisualizeAngleThreshold(playerPos, playerToTriggerDir);
        }
        else {
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
            VisualizeAngleThreshold(playerPos, playerToTriggerDir);
        }
    }

    void VisualizeAngleThreshold(Vector3 playerPos, Vector3 playerToTriggerDir)
    {
        Handles.DrawWireArc(playerPos, Vector3.forward, playerToTriggerDir, angleThresholdDegrees, 1f);
        Handles.DrawWireArc(playerPos, Vector3.forward, playerToTriggerDir, -angleThresholdDegrees, 1f);
    }
    
    void DrawLookLine(Vector2 lookDir)
    {
        Vector2 center = transform.position;
        Vector2 playerPos = playerObjTransform.position; // x axis: works with 2D
        Vector2 playerToTriggerDir = (center - playerPos).normalized;

        // Get angle between the vectors
        float dot = Vector2.Dot(playerToTriggerDir, lookDir);
        dot = Mathf.Clamp(dot, -1, 1);
        float angleInRadians = Mathf.Acos(dot);
        
        // Test if < angular threshold
        float angleThresholdRad = angleThresholdDegrees * Mathf.Deg2Rad;
        bool isLooking = angleInRadians < angleThresholdRad;
        Gizmos.color = isLooking ? Color.green : Color.red;
        Gizmos.DrawLine(playerPos, playerPos + lookDir);
    }
}
