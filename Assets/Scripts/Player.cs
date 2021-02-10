using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Transform shootTarget;

    private void Start()
    {
        Selection.activeObject = playerCollider.gameObject;
    }

    public Transform ShootTarget { get => shootTarget; private set => shootTarget = value; }
}
