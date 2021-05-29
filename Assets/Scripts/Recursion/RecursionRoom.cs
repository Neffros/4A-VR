using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Recursion
{
    public class RecursionRoom : MonoBehaviour
    {

        public List<GameObject> movableObjects = new List<GameObject>();
        public GameObject floorObject;
        public delegate void ChildHandler(GameObject child);


        private float _yScaleFloor;

        public float YScaleFloor => _yScaleFloor;

        public bool isMainRoom;
        // Start is called before the first frame update
        void Start()
        {
            if (isMainRoom) RecursionGameManager.Instance.MainRoom = this;
            else return;
            GetMovableChildObjects(gameObject,delegate(GameObject child) {  },movableObjects, true);
            _yScaleFloor = floorObject.transform.localScale.y;
            foreach (var mov in movableObjects)
            {
                Debug.Log(mov.name);
            }
        }


        private static List<GameObject> GetMovableChildObjects(GameObject gameObject, ChildHandler childHandler, List<GameObject> movableObjects, bool recursion)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.GetComponent<RecursionMovable>() != null)
                {
                    movableObjects.Add(child.gameObject);
                    child.GetComponent<RecursionMovable>().enabled = false;
                }
                childHandler(child.gameObject);
                if(recursion) GetMovableChildObjects(child.gameObject, childHandler, movableObjects,true);
            }

            return movableObjects;
        }
    }

}

