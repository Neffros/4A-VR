using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EndGameUIManager : MonoBehaviour
{

    public GameObject endGamePanel;
    
    public TextMeshProUGUI scoreTxt;

    public TextMeshProUGUI hiScoreTxt;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.EndGameUiManager = this;
    }

   public void UpdateScore()
    {
        scoreTxt.SetText(_gameManager.GameData.Score.ToString());
        hiScoreTxt.SetText(_gameManager.GameData.HighScore.ToString());
    }

   public void PlayAgain()
   {
       
       _gameManager.SoundManager.Play("selectOption");
       _gameManager.ResetData();
       endGamePanel.SetActive(false);
       SceneManager.LoadScene(2);
   }
   
   public void ReturnToMainMenu()
   {
       _gameManager.SoundManager.Play("selectOption");
       _gameManager.ResetData();
       endGamePanel.SetActive(false);
       SceneManager.LoadScene(1);
   }
   
    
    
}
