using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDetection : MonoBehaviour
{
    public delegate void OnPathExited();
    public static event OnPathExited OnPathExitedEvent;

    private bool enteredStartZone;
    internal void OnEnteredStartZone()
    {
        enteredStartZone = true;
    }

    internal void OnExitedPath()
    {
        if (enteredStartZone)
        {
            OnPathExitedEvent();
            enteredStartZone = false;
        }
    }

    internal void OnEnteredPath()
    {
    }

    internal void OnExitedStartZone()
    {

    }
}
