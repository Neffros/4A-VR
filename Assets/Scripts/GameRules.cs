using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    
    private  bool _playerLost;
    private bool _finished;
    private GameManager _gameManager;

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
       // _gameManager.EndGameUiManager.UpdateScore();
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

        Debug.Log("LOST");
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
