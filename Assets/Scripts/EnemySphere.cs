using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySphere : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
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

    public void ShootPlayer(Vector3 target)
    {
        Bullet clone = Instantiate(bulletPrefab);
        clone.transform.position = transform.position;
        clone.Shoot((target - transform.position).normalized);
    }
}
