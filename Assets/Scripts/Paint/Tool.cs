using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESGI.ProjetAnnuel
{
    public abstract class Tool : MonoBehaviour
    {
        public abstract void ApplyEffect(ToolTarget target);
    }
}



