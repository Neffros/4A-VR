using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;



public class ControllerManager : MonoBehaviour
{

    private InputDevice[] _controllers = new InputDevice[2];
    private Animator[] _animators = new Animator[2];

    public Camera vrCamera;

    private GameObject[] spawnedControllers = new GameObject[2];
    private ControllerDict controllerDict;

    public ActionBasedController[] XRControllers;

    private bool[] _isAnimated = new bool[2];
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Grip = Animator.StringToHash("Grip");

    //private static ControllerManager _instance;
    //public static ControllerManager Instance => _instance;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.pauseUIManager.VRCamera = vrCamera;
        TryInitialize(0);
        TryInitialize(1);
        
        
        //if (controllerDict.controllerType == ControllerType.Brush)
          //  Instantiate(controllerDict.controllerPrefab, transform);
        GameManager.Instance.GameData.controllerManager = this;
    }

    public Animator[] Animators => _animators;

    public void TryInitialize(int index)
    {


        controllerDict = GameManager.Instance.GameData.GetControllerDicts(index);
        if (spawnedControllers[index])
        {
            Debug.Log("destroying controller");
            Destroy(spawnedControllers[index]);
        }

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerDict.controllerCharacteristics, devices);

        foreach (var device in devices) 
        {
            Debug.Log(device.name + " was added with char " + device.characteristics);
        }

        if (devices.Count > 0)
        {
            _controllers[index] = devices[0];
            spawnedControllers[index] = Instantiate(controllerDict.controllerPrefab, XRControllers[index].transform);

            //Debug.Log("CONTROLLER HAS ANIMATOR: " + controllerDict.hasAnimator);
            if (controllerDict.hasAnimator)
            {
                Debug.Log("getting component in object: " + spawnedControllers[index].gameObject.name);
                _animators[index] = spawnedControllers[index].GetComponent<Animator>();
                _isAnimated[index] = true;
            }

            if (controllerDict.hasRayInteractor)
            {
                XRInteractorLineVisual ray =
                    XRControllers[index].GetComponent<XRInteractorLineVisual>();
                LineRenderer lineRenderer = XRControllers[index].GetComponent<LineRenderer>();
                ray.enabled = true;
                lineRenderer.enabled = true;
                Debug.Log("ray object name is : " + ray.gameObject.name);
                Debug.Log("line renderer object name is : " + ray.gameObject.name);
                
            }
        }
    }

    //the dict defines if it's the left or right one that is changed
    public void ChangeController(ControllerDict controllerDict)
    {
        int index = GetIndexFromDict(controllerDict);
        GameManager.Instance.GameData.SetControllerDict(controllerDict, index);
        TryInitialize(index);
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

    public int GetIndexFromDict(ControllerDict controllerDict)
    {
        return ((controllerDict.controllerCharacteristics & InputDeviceCharacteristics.Controller) != 0 &&
                     (controllerDict.controllerCharacteristics & InputDeviceCharacteristics.Left) != 0)? 0 : 1;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _isAnimated.Length; i++)
        {
            //Debug.Log("controller " + i + " is valid: " + _controllers[i].isValid);
            if (!_controllers[i].isValid)
            {
                //Debug.Log("controller " + i + " is invalid, reinit");
                TryInitialize(i);
                return;
            }
            if (_isAnimated[i]) UpdateHandAnimator(i);
        }

        /*_controllers[0].TryGetFeatureValue(CommonUsages.menuButton, out bool paused);
        if (paused)
            GameManager.Instance.pauseUIManager.OnPause();    
        _controllers[0].TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stickVal);
        Debug.Log("x" + stickVal.x);
        Debug.Log("y" + stickVal.y);
        Debug.Log("paused" + paused);
        if (stickVal.x > 1f)
        {
            var transform1 = vrCamera.transform;
            var rotation = transform1.rotation;
            rotation = new Quaternion(rotation.x,rotation.y+90f,rotation.z,rotation.w );
            transform1.rotation = rotation;
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

    public InputDevice[] Controllers => _controllers;
}
