﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            GameManager.Instance.GameRules.LoseLevel();
        }
    }
    
}
