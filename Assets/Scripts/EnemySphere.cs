using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySphere : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float waitDurationBeforeShootingAgain = 5;

    private float elapsedTime;
    private int swordLayer;
    private float bulletSpeed;

  

    private void Awake()
    {
        elapsedTime = waitDurationBeforeShootingAgain;
        swordLayer = LayerMask.NameToLayer("Sword");
        bulletSpeed = bulletPrefab.Speed;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
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
        if(elapsedTime >= waitDurationBeforeShootingAgain)
        {
            GameManager.Instance.SoundManager.Play("shoot");
            elapsedTime = 0;
            if (bulletSpeed < 5)
            {
                if (GameManager.Instance.GameData.Score != 0 &&GameManager.Instance.GameData.Score % 5 == 0) 
                    bulletSpeed += 1.0f;
            }
            Bullet clone = Instantiate(bulletPrefab);
            clone.transform.position = transform.position;
            clone.Shoot((target - transform.position).normalized);
        }
    }
}
