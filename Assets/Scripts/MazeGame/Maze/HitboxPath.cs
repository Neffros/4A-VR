using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mazeGame
{
    public class HitboxPath : MonoBehaviour
    {
        [SerializeField] private HitboxDetection hitboxDetection;

        private int swordLayer;

        private void Awake()
        {
            swordLayer = LayerMask.NameToLayer("Sword");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == swordLayer)
            {
                hitboxDetection.OnEnteredPath();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == swordLayer)
            {
                hitboxDetection.OnExitedPath();
            }
        }
    }
}