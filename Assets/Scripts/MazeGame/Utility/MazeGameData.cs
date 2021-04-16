using System.Collections;
using System.Collections.Generic;
using mazeGame;
using UnityEngine;

namespace mazeGame
{
    
    public class MazeGameData : MonoBehaviour
    {
        private int health = 3;
        private int _highScore;
        private int _score = 0;
        private float _timer = 20.00f;
        private int _levelsPlayed = 0;
    
    
        private void Start()
        {
            MazeGameManager.Instance.GameData = this;
            Load();
        }

        public int Health
        {
            get => health;
            set => health = value;
        }

        public int HighScore
        {
            get => _highScore;
            set => _highScore = value;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }

        public float Timer
        {
            get => _timer;
            set => _timer = value;
        }

        public void Reset()
        {
            health = 3;
            Load();
            _score = 0;
            _timer = 20.00f;
            _levelsPlayed = 0;
        }
    
        public void Load()
        {
            _highScore = PlayerPrefs.GetInt("hiscore");
        }

        public void Save()
        {
            PlayerPrefs.SetInt("hiscore", _highScore);
            PlayerPrefs.Save();
        }

        public int LevelsPlayed
        {
            get => _levelsPlayed;
            set => _levelsPlayed = value;
        }

        private void Update()
        {
            if (MazeGameManager.Instance.GameRules.Finished) return;
            if (!MazeGameManager.Instance.GameRules.Started) return;

            _timer -= Time.deltaTime;
        }
    }

}
