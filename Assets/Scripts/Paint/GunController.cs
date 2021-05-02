using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GunController : MonoBehaviour
{
    public float shootFrequency;
    public float speed;
    public GameObject bullet;
    private InputDevice controller;

    private Vector3 playerDirection = new Vector3();

    private void Start()
    {
        controller = GameManager.Instance.GameData.controllerManager.Controllers[0];
    }

    private void Shoot()
    {
        GameObject instance = Instantiate(bullet);
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        playerDirection = GameManager.Instance.GameData.controllerManager.gameObject.transform.forward;
        rb.AddForce(playerDirection * speed, ForceMode.VelocityChange);
    }
    private void Update()
    {
        
        controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.5f)
        {
            Debug.Log("shooting");
            Shoot();
        }
    }
}
