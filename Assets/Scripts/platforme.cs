using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platforme : MonoBehaviour
{
    public Transform seatedSpawnPoint;
    public Transform standingSpawnPoint;
    public AudioSource spawnAudio;

    public Transform SeatedSpawnPoint => seatedSpawnPoint;

    public Transform StandingSpawnPoint => standingSpawnPoint;


    public void PlaySource()
    {
        spawnAudio.Play();
    }

    public void StopSource()
    {
        spawnAudio.Stop();
    }
}
