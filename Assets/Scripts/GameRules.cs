﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameRules : MonoBehaviour
{
    
    private  bool _playerLost;
    private bool _finished;
    private GameManager _gameManager;
    
    private XRInteractorLineVisual _playerRayVisual;
    private LineRenderer _playerRay;
    
    public delegate void Response();
    public static event Response OnNextLevel;
    public static event Response OnLevelLost;
    public static event Response OnLevelWon;

    private bool enteredZone;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.GameRules = this;

        HitboxDetection.OnStartZoneEntered += OnStartZoneEntered;
        HitboxDetection.OnPathExitedEvent += OnPathExited;
    }

    private void OnPathExited()
    {
        enteredZone = false;
        Debug.LogWarning("RIEN D'IMPLEMENTÉ QUAND LE JOUEUR SORT DU CHEMIN !!");
    }

    private void OnStartZoneEntered()
    {
        Debug.Log("StartZone entered");
        enteredZone = true;
    }

    private void Endgame()
    {
        _playerLost = false;
        _finished = true;
        _playerRay = FindObjectOfType<LineRenderer>();
        _playerRay.enabled = true;

        _playerRayVisual = FindObjectOfType<XRInteractorLineVisual>();
        _playerRayVisual.enabled = true;
       _gameManager.EndGameUiManager.endGamePanel.SetActive(true);
       _gameManager.EndGameUiManager.UpdateScore();
       _gameManager.LevelManager.DestroyLevel();
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
            enteredZone = true;
            LoseLevel();
        }
        if (_gameManager.GameData.Health == 0)
        {
            _playerLost = true;
        }
    }

    public void NextLevel()
    {
        enteredZone = false;
        _gameManager.GameData.Timer = 20.0f;
        _gameManager.LevelManager.NextPattern();
        OnNextLevel?.Invoke();
        
    }

    public void WinLevel()
    {
        if (!enteredZone)
        {
            Debug.Log("WIN");
            return;
        }
        _gameManager.GameData.Score++;
        _gameManager.GameData.Timer = 20.0f;
        OnLevelWon?.Invoke();
        NextLevel();
    }
    public void LoseLevel()
    {
        if (!enteredZone)
        {
            Debug.Log("Le joueur n'est pas passé par l'entrée ! (LOST)");
            return;
        }
        _gameManager.GameData.Health--;
        OnLevelLost?.Invoke();
        _gameManager.SoundManager.Play("hitObstacle");
        Debug.Log("LOST");
        NextLevel();
    }

    public void Reset()
    {
        _playerLost = false;
        _finished = false;
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
