﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    
    private  bool _playerLost;
    private bool _finished;
    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.GameRules = this;
        
    }
    
    private void Endgame()
    {
        _gameManager.EndGameUiManager.UpdateScore();
        _playerLost = false;
        //do stuff
    }

    private void Update()
    {
        if (_finished) return;
        if (_playerLost)
        {
            _finished = true;
            Endgame();
            return;
        }

        if (_gameManager.GameData.Timer <= 0)
        {
            LoseLevel();
        }
        if (_gameManager.GameData.Health == 0)
        {
            _playerLost = true;
        }
    }

    public void NextLevel()
    {
        //delete current level
        
        //generateLevel or get level 
    }

    public void WinLevel()
    {
        _gameManager.GameData.Score++;
        _gameManager.GameData.Timer = 20.0f;
        NextLevel();
    }
    public void LoseLevel()
    {
        _gameManager.GameData.Health--;
        NextLevel();
    }
    public bool PlayerLost
    {
        get => _playerLost;
        set => _playerLost = value;
    }

    public bool Finished
    {
        get => _finished;
        set => _finished = value;
    }
}
