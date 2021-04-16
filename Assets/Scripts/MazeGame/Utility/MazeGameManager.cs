using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  mazeGame
{
    public class MazeGameManager : MonoBehaviour
    {
        public GameRules GameRules;
        public EndGameUIManager EndGameUiManager;
        public LevelManager LevelManager { get; internal set; }
        public MazeGameData GameData;
        public UIManager UiManager;

        #region Singleton

        private static MazeGameManager _instance;

        public static MazeGameManager Instance => _instance;

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
        
        public void ResetData()
        {
            GameData.Reset();
            GameRules.Reset();

        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
