using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class LookTrigger : MonoBehaviour
{
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

        float lookCloseness = Vector2.Dot(playerToTriggerDir, playerLookDir);
        bool isLooking = lookCloseness >= preciseness;

        Gizmos.color = isLooking ? activatedColor : deactivatedColor;
        Gizmos.DrawLine(playerPos, playerPos + playerToTriggerDir);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerPos, playerPos + playerLookDir);
    }
}
