﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace mazeGame
{
    public class MenuManager : MonoBehaviour
    {

        private XRController xrController;

        public GameObject mainMenuPanel;
        public GameObject settingsPanel;
        public Dropdown mainHand;
        public Dropdown playMode;


        public ControllerDict leftHandDict;
        public ControllerDict rightHandDict;
        public ControllerDict leftSwordDict;
        public ControllerDict rightSwordDict;
        private void Start()
        {
            MazeGameManager.Instance.GameRules.Started = false;
        }

        public void StartGame()
        {
            GameData globalGameData = GameManager.Instance.GameData;
            MazeGameManager.Instance.GameRules.Started = true;
            GameManager.Instance.SoundManager.Play("selectOption");
            
            if (globalGameData.LeftHand)
            {
                globalGameData.controllerManager.ChangeController(leftSwordDict);
                globalGameData.controllerManager.ChangeController(rightHandDict);
            }
            else
            {
                globalGameData.controllerManager.ChangeController(leftHandDict);
                globalGameData.controllerManager.ChangeController(rightSwordDict);
            }

            SceneManager.LoadScene(2); //TODO change to definitive
        }

        public void DisplaySettings()
        {
            GameManager.Instance.SoundManager.Play("selectOption");
            settingsPanel.gameObject.SetActive(true);
            mainMenuPanel.gameObject.SetActive(false);
        }

        public void QuitGame()
        {
            GameManager.Instance.GameData.Save();
            GameManager.Instance.SoundManager.Play("selectOption");
            SceneManager.LoadScene(4);
            //Application.Quit();
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

    }
}