using System;
using System.Collections;
using System.Collections.Generic;
using mazeGame;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public static GameManager Instance => _instance;

    public LevelManager LevelManager { get; internal set; }

    [HideInInspector] public SceneManager SceneManager;
    public UIManager UiManager;
    public GameData GameData;
    public EndGameUIManager EndGameUiManager;
    public GameRules GameRules;
    public SoundManager SoundManager;
    public ControllerDict baseDictLeft;
    public ControllerDict baseDictRight;
    
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

       
    }

    private void Start()
    {
        GameData.SetControllerDict(baseDictLeft, 0);
        GameData.SetControllerDict(baseDictRight, 1);
        SceneManager = new SceneManager();
        SceneManager.LoadScene(4);
    }

    public void ResetData()
    {
        GameData.Reset();
        GameRules.Reset();

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
