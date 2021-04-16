using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update

    private XRController xrController;

    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public Dropdown mainHand;
    public Dropdown playMode;


    public ControllerDict leftHandDict;
    public ControllerDict rightHandDict;
    public ControllerDict leftSwordDict;
    public ControllerDict rightSwordDict;

    public void ReturnLobby()
    {
        SceneManager.LoadScene("LevelLobby");
    }

    public void Continue()
    {
        mainMenuPanel.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ExitSettings()
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

        settingsPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);
    }


    public void DisplaySettings()
    {
        GameManager.Instance.SoundManager.Play("selectOption");
        settingsPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }
}

