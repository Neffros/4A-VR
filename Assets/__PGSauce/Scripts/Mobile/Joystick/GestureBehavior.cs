using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using NaughtyAttributes;

namespace PGSauce.Mobile
{
    public class GestureBehavior : MonoBehaviour
    {
        [SerializeField] [Required] TouchGameplay touchGameplay;

        public void OnMoveStart(LeanFinger finger)
        {
            touchGameplay.OnMoveStart(finger);
        }

        public void OnMove(LeanFinger finger, Vector2 delta)
        {
            touchGameplay.OnMove(finger, delta);
        }

        public void OnMoveEnd(LeanFinger finger)
        {
            touchGameplay.OnMoveEnd(finger);
        }

        public void OnShortTap(LeanFinger finger)
        {
            touchGameplay.OnShortTap(finger);
        }
    }
}
