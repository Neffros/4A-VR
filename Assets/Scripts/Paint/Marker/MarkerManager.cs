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
        Debug.Log("changing marker");
        _markerInteractor = interactable.GetComponent<MarkerInteractor>();

        if (!_markerInteractor) return;
        
        //_markerInteractor.leftMarker.controllerPrefab.
        
        if (_index == 0)
        {
            
            _markerInteractor.leftMarker.controllerCharacteristics &= ~InputDeviceCharacteristics.Left;
            _markerInteractor.leftMarker.controllerCharacteristics |= InputDeviceCharacteristics.Right;
            //_markerInteractor.leftMarker;
            _controllerManager.InitMarker(_markerInteractor.leftMarker);
            _controllerManager.ChangeController(_markerInteractor.leftMarker);
        }
        else
        {
            _controllerManager.InitMarker(_markerInteractor.rightMarker);
            _controllerManager.ChangeController(_markerInteractor.rightMarker);
        }
    }
}
