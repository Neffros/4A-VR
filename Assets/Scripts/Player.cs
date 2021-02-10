using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Transform shootTarget;

    public Transform ShootTarget { get => shootTarget; private set => shootTarget = value; }
}
