using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("hit " + other.name, other);
    }
    
}
