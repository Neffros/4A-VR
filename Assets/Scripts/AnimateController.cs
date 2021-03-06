﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimateController : MonoBehaviour
{
    public bool showSword;
    public bool showController;
    public bool isLeftHand;
    public InputDeviceCharacteristics controllerChars;
    public GameObject controllerPrefab;
    public GameObject handPrefab;
    public GameObject swordPrefab;
    private InputDevice _controller;
    private Animator _handAnimator;
    
    private GameObject spawnedController;
    private GameObject spawnedHand;
    private GameObject spawnedSword;

    private bool animate;
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Grip = Animator.StringToHash("Grip");

    // Start is called before the first frame update
    void Start()
    {
       TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerChars, devices);

        foreach (var device in devices)
        {
            Debug.Log(device.name + " was added with char " + device.characteristics);
            
        }

        if (devices.Count > 0)
        {
            _controller = devices[0];

            if (GameManager.Instance.GameData.LeftHand && isLeftHand)
            {
                spawnedSword = Instantiate(swordPrefab, transform);
            }
            else if (!GameManager.Instance.GameData.LeftHand && !isLeftHand)
            {
                spawnedSword = Instantiate(swordPrefab, transform);
            }
            else 
            {
                spawnedController = Instantiate(controllerPrefab, transform);
                spawnedHand = Instantiate(handPrefab, transform);
                _handAnimator = spawnedHand.GetComponent<Animator>();
                XRInteractorLineVisual ray =
                    gameObject.GetComponentInParent<XRInteractorLineVisual>();
                ray.enabled = true;
                animate = true;
            }
        }
    }

    void UpdateHandAnimator()
    {
        if (_controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
           _handAnimator.SetFloat(Trigger, triggerValue);
        }
        else
        {
            _handAnimator.SetFloat(Trigger, triggerValue);
        }
        if (_controller.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _handAnimator.SetFloat(Grip, gripValue);
        }
        else
        {
            _handAnimator.SetFloat(Grip, gripValue);
        }
    }

    void UpdateActiveController()
    {
        if (showController)
        {
            spawnedHand.SetActive(false);
            spawnedController.SetActive(true);
        }
        else
        {
            spawnedController.SetActive(false);
            spawnedHand.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!_controller.isValid)
        {
            TryInitialize();
            return;
        }

        if (animate)
        {
            UpdateHandAnimator();  
            UpdateActiveController();
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
