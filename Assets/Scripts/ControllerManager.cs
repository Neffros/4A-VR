﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;



public class ControllerManager : MonoBehaviour
{

    public ControllerDict baseDictLeft;
    public ControllerDict baseDictRight;

    private InputDevice[] _controllers = new InputDevice[2];
    private Animator[] _animators;
    

    private GameObject spawnedController;
    private ControllerDict controllerDict;

    public ActionBasedController[] XRControllers;

    private bool[] _isAnimated = new bool[2];
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Grip = Animator.StringToHash("Grip");

    private void Awake()
    {
        
        /*if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;*/
        DontDestroyOnLoad(gameObject);
    }
    //private static ControllerManager _instance;
    //public static ControllerManager Instance => _instance;

    // Start is called before the first frame update
    void Start()
    {

        controllerDict = baseDictLeft;
        TryInitialize();
        controllerDict = baseDictRight;
        TryInitialize();
        
        //if (controllerDict.controllerType == ControllerType.Brush)
          //  Instantiate(controllerDict.controllerPrefab, transform);
        GameManager.Instance.GameData.controllerManager = this;
    }

    public void TryInitialize()
    {
        Debug.Log("in scene:" + SceneManager.GetActiveScene().name);
        //Debug.Log("CALLING INIT FROM:" + gameObject.name);
        int index = controllerDict.isLeftHanded ? 0 : 1;
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerDict.controllerCharacteristics, devices);

        foreach (var device in devices)
        {
            Debug.Log(device.name + " was added with char " + device.characteristics);
        }

        if (devices.Count > 0)
        {
            _controllers[index] = devices[0];
            spawnedController = Instantiate(controllerDict.controllerPrefab, XRControllers[index].transform);

            //Debug.Log("CONTROLLER HAS ANIMATOR: " + controllerDict.hasAnimator);
            if (controllerDict.hasAnimator)
            {
                Transform test = spawnedController.GetComponent<Transform>();
                //Debug.Log("got transfrom in object: " + test.gameObject.name);
                //Debug.Log("getting component in object: " + spawnedController.gameObject.name);
                _animators[index] = spawnedController.GetComponent<Animator>();
                _isAnimated[index] = true;
            }

            if (controllerDict.hasRayInteractor)
            {
                /*XRInteractorLineVisual ray =
                    XRControllers[index].GetComponentInParent<XRInteractorLineVisual>();
                ray.enabled = true;
                //Debug.Log("ray object name is : " + ray.gameObject.name);
                */
            }
        }
    }

    //the dict defines if it's the left or right one that is changed
    public void ChangeController(ControllerDict controllerDict)
    {
        Debug.Log("CONTROLLER CHANGE");
        this.controllerDict = controllerDict;
        TryInitialize();
    }
    void UpdateHandAnimator(int index)
    {
        if (_controllers[index].TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            _animators[index].SetFloat(Trigger, triggerValue);
        }
        else
        {
            _animators[index].SetFloat(Trigger, triggerValue);
        }
        if (_controllers[index].TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _animators[index].SetFloat(Grip, gripValue);
        }
        else
        {
            _animators[index].SetFloat(Grip, gripValue);
        }
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _isAnimated.Length; i++)
        {
            //Debug.Log("controller " + i + " is valid: " + _controllers[i].isValid);
            if (!_controllers[i].isValid)
            {
                Debug.Log("controller " + i + " is invalid, reinit");
                //TryInitialize();
                return;
            }
            if (_isAnimated[i]) UpdateHandAnimator(i);
        }

        
        /*_controller.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
            Debug.Log("pressing main button in right controller");

        _controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
            Debug.Log("trigger!");
        
        _controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxis);
        if (primary2DAxis != Vector2.zero)
            Debug.Log("moving stick");*/
    }
}
