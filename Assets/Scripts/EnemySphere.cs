using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySphere : MonoBehaviour
{
    private int swordLayer;
    private void Awake()
    {
        swordLayer = LayerMask.NameToLayer("Sword");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == swordLayer)
        {
            GameManager.Instance.GameRules.WinLevel();
        }
    }
}
