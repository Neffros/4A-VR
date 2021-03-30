using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mazeGame
{

    public class platforme : MonoBehaviour
    {
        public Transform seatedSpawnPoint;
        public Transform standingSpawnPoint;
        public AudioSource spawnAudio;

        private float _scale;
        public Transform SeatedSpawnPoint => seatedSpawnPoint;

        public Transform StandingSpawnPoint => standingSpawnPoint;

        private void Start()
        {
            _scale = gameObject.transform.localScale.x;
        }

        public void PlaySource()
        {
            spawnAudio.Play();
        }

        public void StopSource()
        {
            spawnAudio.Stop();
        }

        public float Scale => _scale;
    }
}