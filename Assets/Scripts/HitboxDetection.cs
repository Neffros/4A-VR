﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitboxDetection : MonoBehaviour
{


    public delegate void Response();
    public static event Response OnPathExitedEvent;
    public static event Response OnStartZoneEntered;

    private bool enteredStartZone;
    
    internal void OnEnteredStartZone()
    {
        enteredStartZone = true;
        OnStartZoneEntered();
    }

    internal void OnExitedPath()
    {
        if (enteredStartZone)
        {
            enteredStartZone = false;
            OnPathExitedEvent();
            GameManager.Instance.SoundManager.Play("Danger");
            GameManager.Instance.GameRules.HasCheated = true;
        }
    }

    internal void OnEnteredPath()
    {

    }

    internal void OnExitedStartZone()
    {
        Debug.Log("Start zone exited");
    }
}
