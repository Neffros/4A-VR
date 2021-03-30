using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ControllerType
{
    Sword,
    Hand,
    Controller,
    Brush
};
/*
[Serializable]
public class ControllerDict : MonoBehaviour
{ 
    public ControllerType controllerType;
    public GameObject controllerPrefab;
}*/


[CreateAssetMenu(fileName = "ControllerDict", menuName = "ScriptableObject/ControllerDict")]
public class ControllerDict : ScriptableObject
{
    public ControllerType controllerType;
    public GameObject controllerPrefab;
    public bool hasAnimator;
}
