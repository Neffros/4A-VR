using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Mobile;

namespace PGSauce.Mobile
{
    [CreateAssetMenu(menuName = "PG/Mobile/Finger Registering Validator/Null")]
    public class NullFingerRegisteringValidator : FingerRegisteringValidator
    {
        public override bool Validate(Lean.Touch.LeanFinger finger)
        {
            return true;
        }
    }
}
