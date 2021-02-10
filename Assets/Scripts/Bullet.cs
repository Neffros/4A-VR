using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool hitOnce;

    internal void Shoot(Vector3 direction)
    {
        rb.AddForce(direction * speed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(hitOnce) { return; }
            hitOnce = true;

            GameManager.Instance.GameRules.HitByBullet();
        }
    }
}
