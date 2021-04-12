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


    public PauseUIManager pauseUIManager;
    public GameData GameData;
    public SoundManager SoundManager;
    
    public ControllerDict baseDictLeft;
    public ControllerDict baseDictRight;
    
    public Collider sword;

    #region Singleton

    private static GameManager _instance;
    
    public static GameManager Instance => _instance;
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

    #endregion


    private void Start()
    {
        GameData.SetControllerDict(baseDictLeft, 0);
        GameData.SetControllerDict(baseDictRight, 1);
        SceneManager.LoadScene(4);
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
