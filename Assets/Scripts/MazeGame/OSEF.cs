using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mazeGame
{

    public class OSEF : MonoBehaviour
    {
        private void Awake()
        {
            HitboxDetection.OnPathExitedEvent += () => { };
        }
    }
}