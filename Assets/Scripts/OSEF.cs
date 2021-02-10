using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSEF : MonoBehaviour
{
    private void Awake()
    {
        HitboxDetection.OnPathExitedEvent += () =>
        {
        };
    }
}
