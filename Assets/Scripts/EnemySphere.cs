using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySphere : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float waitDurationBeforeShootingAgain = 5;

    private float elapsedTime;
    private int swordLayer;
    private void Awake()
    {
        elapsedTime = waitDurationBeforeShootingAgain;
        swordLayer = LayerMask.NameToLayer("Sword");
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
            elapsedTime = 0;
            Bullet clone = Instantiate(bulletPrefab);
            clone.transform.position = transform.position;
            clone.Shoot((target - transform.position).normalized);
        }
    }
}
