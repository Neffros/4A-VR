using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitboxDetection : MonoBehaviour
{
    [SerializeField] private GameObject obstaclesParent;
    [SerializeField] private HitboxPath hitboxPath;
    [SerializeField] private HitboxStart hitboxStart;
    [SerializeField] private EnemySphere enemySphere;

    public delegate void Response();
    public static event Response OnPathExitedEvent;
    public static event Response OnStartZoneEntered;

    private bool enteredStartZone;

    private List<Collider> colliders;

    private void Awake()
    {
        CreateCollidersList();

        GameRules.OnLevelWon += DisableCollisions;
        GameRules.OnLevelLost += DisableCollisions;

        GameRules.OnNextLevel += EnableCollisions;
    }

    private void CreateCollidersList()
    {
        colliders = obstaclesParent.GetComponentsInChildren<Collider>().ToList();
        colliders.Add(hitboxPath.gameObject.GetComponent<Collider>());
        colliders.Add(hitboxStart.gameObject.GetComponent<Collider>());
        colliders.Add(enemySphere.gameObject.GetComponent<Collider>());
    }

    public void EnableCollisions()
    {
        foreach (var col in colliders)
        {
            col.enabled = true;
        }
    }

    public void DisableCollisions()
    {
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
    }

    internal void OnEnteredStartZone()
    {
        enteredStartZone = true;
        OnStartZoneEntered();
    }

    internal void OnExitedPath()
    {
        if (enteredStartZone)
        {
            enteredStartZone = false;
            OnPathExitedEvent();
        }
    }

    internal void OnEnteredPath()
    {

    }

    internal void OnExitedStartZone()
    {
        Debug.Log("Start zone exited");
    }
}
