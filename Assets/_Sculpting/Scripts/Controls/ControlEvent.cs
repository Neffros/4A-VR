using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class ControlEvent : MonoBehaviour
    {
        [Serializable]
        public class UnityEventActionBasedController : UnityEvent<ActionBasedController> {}
        private enum ButtonType
        {
            Trigger,
            Grip
        }
        [SerializeField] private ActionBasedController controller;
        [SerializeField] private ButtonType buttonType;
        [SerializeField] private UnityEventActionBasedController onBegin;
        [SerializeField] private UnityEventActionBasedController onHold;
        [SerializeField] private UnityEventActionBasedController onEnd;
        private bool _wasPressed;

        public void UpdateControl()
        {
            var inputActionProperty = GetInputActionProperty();
            var value = inputActionProperty.action.ReadValue<float>();
            var isPressed = value > 0.1f;

            if(isPressed && !_wasPressed)
            {
                onBegin.Invoke(controller);
            }
            if (isPressed)
            {
                onHold.Invoke(controller);
            }
            if (!isPressed && _wasPressed)
            {
                onEnd.Invoke(controller);
            }
            
            _wasPressed = isPressed;
        }

        private InputActionProperty GetInputActionProperty()
        {
            switch (buttonType)
            {
                case ButtonType.Trigger :
                    return controller.activateAction;
                case ButtonType.Grip:
                    return controller.selectAction;
            }

            throw new UnityException($"{buttonType} does not exist !");
        }
    }
}
