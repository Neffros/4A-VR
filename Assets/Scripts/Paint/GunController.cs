using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float shootFrequency;
    public float speed;
    public GameObject bullet;


    private Vector3 playerDirection = new Vector3();
    private void Shoot()
    {
        GameObject instance = Instantiate(bullet);
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        playerDirection = GameManager.Instance.GameData.controllerManager.gameObject.transform.forward;
        rb.AddForce(playerDirection * speed, ForceMode.VelocityChange);
    }
    private void Update()
    {
        
        if(Input.anyKey)
            Shoot();
    }
}
