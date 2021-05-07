using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

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
    private bool gunIsGrabbed;
    
    private void Start()
    {
        //bullet.GetComponent<CollisionPainter>().paintColor = bulletColor;
        //bullet.GetComponent<MeshRenderer>().material.color = bulletColor;
        shootTransform.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
    }

    public void OnGunSelect()
    {
        controller = GameManager.Instance.GameData.controllerManager.Controllers[PaintManager.instance.ControllerIndex];
        Debug.Log(gameObject.name + " grabbed");
        gunIsGrabbed = true;
    }

    public void OnGunExit()
    {
        Debug.Log(gameObject.name + " dropped");
        gunIsGrabbed = false;
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
        if (!gunIsGrabbed) return;
        controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.5f && shootFrequency <= elapsedTime)
        {
            Debug.Log("shoot");
            Shoot();
            elapsedTime = 0f;
        }
    }
}
