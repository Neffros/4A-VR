using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private int health = 3;
    private int _highScore = 0;
    private int _score = 0;
    private float _timer = 20.00f;

    private void Start()
    {
        GameManager.Instance.GameData = this;
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

    private void Update()
    {
        _timer -= Time.deltaTime;
    }
}
