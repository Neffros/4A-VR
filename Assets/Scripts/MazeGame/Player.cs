using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace mazeGame
{

    public class Player : MonoBehaviour
    {
        [SerializeField] private Collider playerCollider;
        [SerializeField] private Transform shootTarget;



        public Transform ShootTarget
        {
            get => shootTarget;
            private set => shootTarget = value;
        }
    }
}