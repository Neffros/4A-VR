using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace mazeGame
{

    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreTxt;
        public TextMeshProUGUI highScoreTxt;
        public TextMeshProUGUI timerTxt;
        public TextMeshProUGUI hpTxt;
        private MazeGameData _gameData;

        private void Start()
        {
            MazeGameManager.Instance.UiManager = this;
            _gameData = MazeGameManager.Instance.GameData;
        }

        private void UpdateUI()
        {
            scoreTxt.SetText(_gameData.Score.ToString());
            highScoreTxt.SetText(_gameData.HighScore.ToString());
            timerTxt.SetText(_gameData.Timer.ToString("0.00"));
            hpTxt.SetText(_gameData.Health.ToString());
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateUI();
        }
    }
}