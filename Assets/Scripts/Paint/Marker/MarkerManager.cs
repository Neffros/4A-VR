using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class MarkerManager : MonoBehaviour
{

    public ControllerDict leftBrush;
    public ControllerDict rightBrush;
    public void OnMarkerInteract(XRBaseInteractable interactable)
    {
        Debug.Log("interactable name: " + interactable.gameObject.name);
        
    }
}
