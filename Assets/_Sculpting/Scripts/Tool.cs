using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public abstract class Tool : MonoBehaviour
    {
        [SerializeField] private ActionBasedController rightHand;

        [SerializeField] private ControlEvent rightTrigger;
        [SerializeField] private ControlEvent rightGrip;

        private bool _wasPressed;
        private bool _canWork;

        public void Enable(bool val)
        {
            _canWork = val;
        }

        protected void Start()
        {
            CustomStart();
        }

        protected virtual void CustomStart()
        {
            
        }

        protected ActionBasedController RightHand => rightHand;

        protected void Update()
        {
            if(!_canWork) {return;}

            if (rightTrigger != null)
            {
                rightTrigger.UpdateControl();
            }

            if (rightGrip != null)
            {
                rightGrip.UpdateControl();
            }
            
            
            CustomUpdate();
        }

        protected virtual void CustomUpdate()
        {
        }

        public virtual void OnRightTriggerBegin()
        {
        }

        public virtual void OnRightTriggerEnd()
        {
        }

        public virtual void OnRightTrigger()
        {
        }
        
        public virtual void OnRightGripBegin()
        {
        }

        public virtual void OnRightGripEnd()
        {
        }

        public virtual void OnRightGrip()
        {
        }
    }
}
