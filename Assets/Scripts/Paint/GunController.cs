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
    public float bulletScale;
    private InputDevice controller;
    public Transform shootTransform;
    private Vector3 playerDirection = new Vector3();
    private float elapsedTime = 0.5f;
    
    private void Start()
    {
        controller = GameManager.Instance.GameData.controllerManager.Controllers[0];
        shootTransform.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
    }

    private void Shoot()
    {
        GameObject instance = Instantiate(bullet, shootTransform);
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        //playerDirection = GameManager.Instance.GameData.controllerManager.vrCamera.transform.forward;
        playerDirection = shootTransform.forward;
        rb.AddForce(playerDirection * speed, ForceMode.VelocityChange);
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;

        controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.5f && shootFrequency <= elapsedTime)
        {
            Debug.Log("shooting");
            Shoot();
            elapsedTime = 0f;
        }
    }
}
