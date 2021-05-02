using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class MarkerManager : MonoBehaviour
{

    private MarkerInteractor _markerInteractor;
    //private Transform objectPos; swap objects if brush mabye 
    private ControllerManager _controllerManager;
    private int _index;
    private void Start()
    {
        _controllerManager = GameManager.Instance.GameData.controllerManager;
    }

    public void SetControllerTarget(int index)
    {
        Debug.Log("settings index");
        _index = index;
    }
    public void OnMarkerInteract(XRBaseInteractable interactable)
    {

    }
}
