using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    public  Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        hitOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
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

            Debug.Log("hit player!");
            hitOnce = true;

            GameManager.Instance.GameRules.HitByBullet();
        }
    }
}
