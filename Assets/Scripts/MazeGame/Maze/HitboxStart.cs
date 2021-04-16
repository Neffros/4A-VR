using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mazeGame
{
    public class HitboxStart : MonoBehaviour
    {
        [SerializeField] private HitboxDetection hitboxDetection;

        private int swordLayer;

        private void Awake()
        {
            swordLayer = LayerMask.NameToLayer("Sword");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == swordLayer)
            {
                MazeGameManager.Instance.LevelManager.Platformes[MazeGameManager.Instance.LevelManager.CurrentPlatformIndex].StopSource();
                hitboxDetection.OnEnteredStartZone();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == swordLayer)
            {
                hitboxDetection.OnExitedStartZone();
            }
        }
    }
}

