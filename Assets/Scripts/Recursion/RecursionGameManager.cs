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
        private List<RecursionRoom> _rooms = new List<RecursionRoom>();
        private List<GameObject> movableTargetObjects = new List<GameObject>();
        public RecursionRoom prefab;
        public Material mainFloorMaterial;

        public GameObject vrBody;
        public GameObject leftHand;
        public GameObject rightHand;
        [SerializeField] private int biggerComplexity;
        [SerializeField] private int smallerComplexity;

        public Vector3 offset;
        public float scale;
        
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

        public List<GameObject> MovableTargetObjects => movableTargetObjects;

        public void SetTargetObjectInList(GameObject newTarget, int index)
        {
            movableTargetObjects[index] = newTarget;
        }

        #endregion
        // Start is called before the first frame update

        private void Start()
        {
            GameObject parent = this.gameObject;
            int totalOfRooms = biggerComplexity + smallerComplexity + 1;
            for (int i = 0; i < totalOfRooms; i++)
            {
                RecursionRoom currentRoom = Instantiate(prefab, parent.transform);
                var transform1 = currentRoom.transform;
                Vector3 pos = transform1.localPosition;
                pos += offset;
                transform1.localPosition = pos;
                transform1.localScale = Vector3.one * scale;
                parent = currentRoom.gameObject;
                _rooms.Add(currentRoom);
            }
            //big 2 main 2 small
            //scale offset of 0.5
            // 1 0.5 0.25 0.125 ...
            //       ----
            // 4  2   1    0.5
        
            // 1 0.5 0.25 0.125 ...
            //             ----
            // 8  4   2    1 
            float fixedScale = Mathf.Pow((1 / 0.5f),biggerComplexity+1);
            _rooms[0].transform.localScale *= fixedScale;
            mainRoom = _rooms[biggerComplexity];
            mainRoom.SetFloorMaterial(mainFloorMaterial);
            Debug.Log(mainRoom.transform.lossyScale);
            mainRoom.name = "Main Room";
            
            movableTargetObjects = mainRoom.movableObjects;
            SetTargetObjectInList(vrBody, 2);
            SetTargetObjectInList(leftHand, 3);
            SetTargetObjectInList(rightHand, 4);
        }
    }

}
