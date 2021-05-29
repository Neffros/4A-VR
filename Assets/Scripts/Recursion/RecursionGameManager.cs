using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace Recursion
{
    public class RecursionGameManager : MonoBehaviour
    {

        private RecursionRoom mainRoom;
        public RecursionRoom prefab;
        public List<RecursionRoom> rooms;
        public Vector3 offset;
        public float scale;
        public Material mainFloorMaterial;
        [SerializeField] private int biggerComplexity;
        
        [SerializeField] private int smallerComplexity;

        [SerializeField] private float scaleValue;
        private bool _generated;
        #region Singleton


        private static RecursionGameManager _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
        }


        public static RecursionGameManager Instance => _instance;

        #endregion

        #region Properties

        public RecursionRoom MainRoom
        {
            get => mainRoom;
            set => mainRoom = value;
        }

        #endregion
        // Start is called before the first frame update


        public void GenerateSmallerLevels(int complexity) //TODO find formula for decreasing scale 
        {
            GameObject newRoom;
            Vector3 newScale;
            Transform newTransform;
            newScale = mainRoom.transform.localScale;

            for (int i = 0; i < complexity; i++)
            {
                newRoom = Instantiate(mainRoom.gameObject);
                newRoom.name = "smaller room " + i;
                newScale.x = newScale.x / -scaleValue <= 0 ? 0.1f : newScale.x / -scaleValue;
                newScale.y = newScale.y / -scaleValue <= 0 ? 0.1f : newScale.y / -scaleValue;
                newScale.z = newScale.z / -scaleValue <= 0 ? 0.1f : newScale.z / -scaleValue;
                //newScale.x /= -scaleValue;
                //newScale.y /= -scaleValue;
                //newScale.z /= -scaleValue;
                newRoom.transform.localScale = newScale;
            }
        }
        
        public void GenerateBiggerLevels(int complexity)
        {
            GameObject newRoom;
            Vector3 newScale;
            Transform newTransform;
            newScale = mainRoom.transform.localScale;
            Vector3 currentFloorScale;
            for (int i = 0; i < complexity; i++)
            {
                newRoom = Instantiate(mainRoom.gameObject);
                
     
                newRoom.name = "bigger room " + i;
                
                newScale.x *= scaleValue;
                newScale.y *= scaleValue;
                newScale.z *= scaleValue;
                
                //keeping Y value of floor 
                currentFloorScale = newRoom.GetComponent<RecursionRoom>().floorObject.transform.localScale;
                Debug.Log("y of main floor" + mainRoom.YScaleFloor);
                //newRoom.GetComponent<RecursionRoom>().floorObject.transform.localScale = 
                //    new Vector3(currentFloorScale.x, mainRoom.YScaleFloor, currentFloorScale.z);

                newRoom.transform.localScale = newScale;
            }
        }
        // Update is called once per frame
        void Update()
        {
            
            if (_generated) return;
            if (mainRoom == null) return;
            GenerateSmallerLevels(smallerComplexity);
            GenerateBiggerLevels(biggerComplexity);
            _generated = true;
        }
    }

}
