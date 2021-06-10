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
        public MeshRenderer floor;
        public List<GameObject> movableObjects = new List<GameObject>();
        public delegate void ChildHandler(GameObject child);
        
        private RecursionGameManager _recursionManager;


        // Start is called before the first frame update
        void Start()
        {
            _recursionManager = RecursionGameManager.Instance;
            /*GetMovableChildObjects(gameObject,delegate(GameObject child) {  },movableObjects, true);
            foreach (var mov in movableObjects)
            {
                Debug.Log(mov.name);
            }*/
        }
        
        public void OnInteractObject(int index) //sphere = 0 cube = 1, body = 2 , left controller = 3, right controller = 4 
        {
            _recursionManager.SetTargetObjectInList(movableObjects[index], index);
        }
        private void FollowMovableObjects()
        {
            for (int i = 0; i < movableObjects.Count; i++)
            {
                movableObjects[i].transform.localPosition = _recursionManager.MovableTargetObjects[i].transform.localPosition;
                movableObjects[i].transform.localRotation = _recursionManager.MovableTargetObjects[i].transform.localRotation;
            }
        }
        
        
        private void Update()
        {
            FollowMovableObjects();
        }

        public void SetFloorMaterial(Material mat)
        {
            floor.material = mat;
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

