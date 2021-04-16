using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUIManager : MonoBehaviour
{

    private Camera vrCamera;

    public Canvas pauseCanvas;
    
    public GameObject pausePanel;
    public GameObject settingsPanel;

    public Dropdown mainHand;
    public Dropdown playMode;

    private void Start()
    {
        GameManager.Instance.pauseUIManager = this;
    }

    public Camera VRCamera
    {
        get => vrCamera;
        set
        {
            vrCamera = value;
            //pauseCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            pauseCanvas.worldCamera = vrCamera;
        }
    }

    public void OnPause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = Time.timeScale == 1? 0 : 1;
        
    }
    public void OnResume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnExitSettings()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.SoundManager.Play("selectOption");

        if (mainHand.value == 0)
            gameManager.GameData.LeftHand = false;
        else
            gameManager.GameData.LeftHand = true;

        if (playMode.value == 0)
            gameManager.GameData.Seated = false;
        else
            gameManager.GameData.Seated = true;
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void OnQuitLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
