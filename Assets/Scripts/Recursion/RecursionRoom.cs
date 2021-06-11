using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

namespace Recursion
{
    public class RecursionRoom : MonoBehaviour
    {
        public MeshRenderer floor;
        public List<GameObject> movableObjects = new List<GameObject>();
        public delegate void ChildHandler(GameObject child);
        
        private RecursionGameManager _recursionManager;
        private Animator[] _animators = new Animator[2];
        private Animator[] _targetAnimators = new Animator[2];
        private static readonly int Trigger = Animator.StringToHash("Trigger");
        private static readonly int Grip = Animator.StringToHash("Grip");

        // Start is called before the first frame update
        void Start()
        {
            _recursionManager = RecursionGameManager.Instance;
            _animators[0] = movableObjects[2].GetComponent<Animator>();
            _animators[1] = movableObjects[3].GetComponent<Animator>();

            _targetAnimators = _recursionManager.animators;
            
            /*GetMovableChildObjects(gameObject,delegate(GameObject child) {  },movableObjects, true);
            foreach (var mov in movableObjects)
            {
                Debug.Log(mov.name);
            }*/
        }

        private void UpdateHandAnimator(int index)
        {
            _animators[index].SetFloat(Trigger, _targetAnimators[index].GetFloat(Trigger));
            _animators[index].SetFloat(Grip, _targetAnimators[index].GetFloat(Grip));
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
            UpdateHandAnimator(0);
            UpdateHandAnimator(1);
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

