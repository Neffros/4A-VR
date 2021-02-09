using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.sword = gameObject.GetComponent<Collider>();
    }
}
