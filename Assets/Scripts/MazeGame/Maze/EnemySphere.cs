using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mazeGame
{
    public class EnemySphere : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float waitDurationBeforeShootingAgain = 5;

        private float elapsedTime;
        private int swordLayer;
        private float bulletSpeed;
        private float tmpBulletSpeed;


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
                MazeGameManager.Instance.GameRules.WinLevel();
            }
        }

        public void ShootPlayer(Vector3 target)
        {
            tmpBulletSpeed = bulletSpeed;
            if (elapsedTime >= waitDurationBeforeShootingAgain)
            {
                GameManager.Instance.SoundManager.Play("shoot");
                elapsedTime = 0;
                if (bulletSpeed < 5)
                {
                    if (MazeGameManager.Instance.GameData.Score != 0 && MazeGameManager.Instance.GameData.Score % 5 == 0)
                        bulletSpeed += 1.0f;
                }

                if (MazeGameManager.Instance.GameRules.HasCheated)
                    bulletSpeed *= 1.5f;

                Bullet clone = Instantiate(bulletPrefab);
                clone.transform.position = transform.position;
                clone.Shoot((target - transform.position).normalized);
                bulletSpeed = tmpBulletSpeed;
            }
        }
    }
}
