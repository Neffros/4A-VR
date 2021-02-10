using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEvent : MonoBehaviour
{
    public void OnActivateEvent()
    {
        Debug.Log("teleporter " + gameObject.name + " used");
    }
}
