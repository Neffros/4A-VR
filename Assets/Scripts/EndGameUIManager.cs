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
       endGamePanel.SetActive(false);
       SceneManager.LoadScene(3);
   }
   
   public void ReturnToMainMenu()
   {
       endGamePanel.SetActive(false);
       SceneManager.LoadScene(1);
   }
   
    
    
}
