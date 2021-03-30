using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;



public class AnimateController : MonoBehaviour
{
    public InputDeviceCharacteristics controllerChars;
    
    public ControllerDict controllerDict; 
    
    private InputDevice _controller;
    private Animator _controllerAnimator;

    private GameObject spawnedController;

    private bool animate;
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Grip = Animator.StringToHash("Grip");

    // Start is called before the first frame update
    void Start()
    {
       TryInitialize();
       if (controllerDict.controllerType == ControllerType.Brush)
           Instantiate(controllerDict.controllerPrefab, transform);
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
            GameManager.Instance.Device = _controller;
            spawnedController = Instantiate(controllerDict.controllerPrefab, transform);
            if (controllerDict.hasAnimator)
            {
                _controllerAnimator = spawnedController.GetComponent<Animator>();
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
           _controllerAnimator.SetFloat(Trigger, triggerValue);
        }
        else
        {
            _controllerAnimator.SetFloat(Trigger, triggerValue);
        }
        if (_controller.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _controllerAnimator.SetFloat(Grip, gripValue);
        }
        else
        {
            _controllerAnimator.SetFloat(Grip, gripValue);
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
