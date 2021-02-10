using System;
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

    private bool startShooting;

    private void OnPathExited()
    {
        enteredZone = false;
        startShooting = true;
    }

    private void Shoot()
    {
        Player player = FindObjectOfType<Player>();
        _gameManager.LevelManager.enemySphere.ShootPlayer(player.ShootTarget.position);
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
        _gameManager.SoundManager.Play("gameover");
        _playerRayVisual = FindObjectOfType<XRInteractorLineVisual>();
        _playerRayVisual.enabled = true;

        if (_gameManager.GameData.Score >= _gameManager.GameData.HighScore)
            _gameManager.GameData.HighScore = _gameManager.GameData.Score;
        
        _gameManager.GameData.Save();
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

        if (_gameManager.GameData.Timer <= 0 || startShooting)
        {
            Shoot();
        }
        if (_gameManager.GameData.Health == 0)
        {
            _playerLost = true;
        }
    }

    public void HitByBullet()
    {
        enteredZone = true;
        LoseLevel();
    }

    public void NextLevel()
    {
        _gameManager.GameData.LevelsPlayed++;
        enteredZone = false;
        startShooting = false;

        if (_gameManager.GameData.Score != 0 && _gameManager.GameData.Score % 5 == 0) //every 5 levels won, win 1hp
        {
            _gameManager.GameData.Health++;
        }
        _gameManager.GameData.Timer = 20.0f - _gameManager.GameData.Score / 2;
        if (_gameManager.GameData.Timer < 10.0f)
            _gameManager.GameData.Timer = 10.0f;
        
        _gameManager.LevelManager.NextPattern();
        OnNextLevel?.Invoke();
    }

    public void WinLevel()
    {
        if (!enteredZone)
        {
            Debug.Log("Le joueur n'est pas passé par l'entrée ! (WIN)");
            return;
        }
        _gameManager.GameData.Score++;
        _gameManager.GameData.Timer = 20.0f;
        _gameManager.SoundManager.Play("success");
        OnLevelWon?.Invoke();
        Debug.Log("WIN");
        NextLevel();
    }
    public void LoseLevel()
    {
        _gameManager.SoundManager.Play("hitObstacle");
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
