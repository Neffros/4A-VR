using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    
    public static GameManager Instance => _instance;

    [HideInInspector] public SceneManager SceneManager;
    public UIManager UiManager;
    public GameData GameData;
    public EndGameUIManager EndGameUiManager;
    public GameRules GameRules;

    public Collider sword;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager = new SceneManager();
        SceneManager.LoadScene(3);
    }

    public void EnableSword()
    {
        sword.enabled = true;
    }

    public void DisableSword()
    {
        sword.enabled = false;
    }
}
