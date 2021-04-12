using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameData : MonoBehaviour
{
    private bool _leftHand;
    private bool _seated;
    private ControllerDict[] controllerDicts = new ControllerDict[2];
    
    
    [HideInInspector] public ControllerManager controllerManager;



    public ControllerDict GetControllerDicts(int index)
    {
        return controllerDicts[index];
    }
    public void SetControllerDict(ControllerDict controllerDict, int index)
    {
        controllerDicts[index] = controllerDict;
    }

    private void Start()
    {
        GameManager.Instance.GameData = this;
        Load();
    }

    public bool LeftHand
    {
        get => _leftHand;
        set => _leftHand = value;
    }

    public bool Seated
    {
        get => _seated;
        set => _seated = value;
    }

    public void Load()
    {
        _leftHand = (PlayerPrefs.GetInt("usedHand") != 0);
        _seated = (PlayerPrefs.GetInt("seated") != 0);
        
    }

    public void Save()
    {
        PlayerPrefs.SetInt("usedHand", _leftHand? 0 : 1);
        PlayerPrefs.SetInt("seated", _seated ? 0 : 1);
        PlayerPrefs.Save();
    }
}
