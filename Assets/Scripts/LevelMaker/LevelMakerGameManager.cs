using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelMaker
{
    
    public class LevelMakerGameManager : MonoBehaviour
    {
        private GameObject _leftSelectedObject;
        private GameObject _rightSelectedObject;
        private int controllerIndex;

        #region Properties

        public GameObject LeftSelectedObject
        {
            get => _leftSelectedObject;
            set => _leftSelectedObject = value;
        }

        public GameObject RightSelectedObject
        {
            get => _rightSelectedObject;
            set => _rightSelectedObject = value;
        }
        public int ControllerIndex
        {
            get => controllerIndex;
            set => controllerIndex = value;
        }

        public void SetControllerIndex(int index)
        {
            controllerIndex = index;
        }

        public static LevelMakerGameManager Instance => _instance;

        #endregion

        #region Singleton

        private static LevelMakerGameManager _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
        }

        #endregion
        
        
    }

}
