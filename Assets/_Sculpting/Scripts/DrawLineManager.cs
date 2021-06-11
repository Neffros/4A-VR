using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public abstract class DrawLineManager : MonoBehaviour
    {
        [SerializeField] private ActionBasedController rightHand;

        private bool _wasPressed;

        protected ActionBasedController RightHand => rightHand;

        private void Update()
        {
            var rightTrigger = rightHand.activateAction.action.ReadValue<float>();
            var isPressed = rightTrigger > 0.1f;

            if(isPressed && !_wasPressed)
            {
                OnRightTriggerBegin();
            }
            if (isPressed)
            {
                OnRightTrigger();
            }
            if (!isPressed && _wasPressed)
            {
                OnRightTriggerEnd();
            }
            
            _wasPressed = isPressed;
        }

        protected virtual void OnRightTriggerBegin()
        {
        }

        protected virtual void OnRightTriggerEnd()
        {
        }

        protected virtual void OnRightTrigger()
        {
        }
    }
}
