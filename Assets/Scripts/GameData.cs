﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private int health = 3;
    private int _highScore;
    private int _score = 0;
    private float _timer = 20.00f;
    private int _levelsPlayed = 0;
    private bool _leftHand;
    private bool _seated;

    [HideInInspector] public ControllerManager controllerManager;
    /*private ControllerManager leftController;
    private ControllerManager rightController;

    public ControllerManager LeftController
    {
        get => leftController;
        set => leftController = value;
    }

    public ControllerManager RightController
    {
        get => rightController;
        set => rightController = value;
    }
*/
    private void Start()
    {
        GameManager.Instance.GameData = this;
        Load();
        
    }

    public int Health
    {
        get => health;
        set => health = value;
    }

    public int HighScore
    {
        get => _highScore;
        set => _highScore = value;
    }

    public int Score
    {
        get => _score;
        set => _score = value;
    }

    public float Timer
    {
        get => _timer;
        set => _timer = value;
    }

    public bool LeftHand
    {
        get => _leftHand;
        set => _leftHand = value;
    }

    public bool Seated
    {
        get => _seated;
        set => _seated = value;
    }

    public void Reset()
    {
        health = 3;
        Load();
        _score = 0;
        _timer = 20.00f;
        _levelsPlayed = 0;
    }
    
    public void Load()
    {
        _highScore = PlayerPrefs.GetInt("hiscore");
        _leftHand = (PlayerPrefs.GetInt("usedHand") != 0);
        _seated = (PlayerPrefs.GetInt("seated") != 0);
        
    }

    public void Save()
    {
        PlayerPrefs.SetInt("hiscore", _highScore);
        PlayerPrefs.SetInt("usedHand", _leftHand? 0 : 1);
        PlayerPrefs.SetInt("seated", _seated ? 0 : 1);
        PlayerPrefs.Save();
    }

    public int LevelsPlayed
    {
        get => _levelsPlayed;
        set => _levelsPlayed = value;
    }

    private void Update()
    {
        if (GameManager.Instance.GameRules.Finished) return;
        if (!GameManager.Instance.GameRules.Started) return;

        _timer -= Time.deltaTime;
    }
}
