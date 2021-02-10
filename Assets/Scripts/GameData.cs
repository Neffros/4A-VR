using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private int health = 3;
    private int _highScore = 5;
    private int _score = 0;
    private float _timer = 20.00f;
    private int _levelsPlayed = 0;
    private bool _leftHand;
    private bool _seated;
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
        _highScore = 0;
        _score = 0;
        _timer = 20.00f;
        _levelsPlayed = 0;
    }
    
    public void Load()
    {
        _highScore = PlayerPrefs.GetInt("hiscore");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("hiscore", _highScore);
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
        _timer -= Time.deltaTime;
    }
}
