using System.Collections;
using System.Collections.Generic;
using LevelMaker;
using UnityEngine;
using UnityEngine.XR;

public class InteractableController : MonoBehaviour
{
    public float speed = 1f;
    private bool _selected;
    private InputDevice controller;
    public void OnObjectSelect()
    {
        controller = GameManager.Instance.GameData.controllerManager.Controllers[LevelMakerGameManager.Instance.ControllerIndex];
        Debug.Log(gameObject.name + " grabbed");
        _selected = true;
    }

    public void OnObjectDeselect()
    {
        Debug.Log(gameObject.name + " dropped");
        _selected = false;
    }
    private void Update()
    {
        if (!_selected) return;
        controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stickValue);
        if (stickValue.x > 0.5f || stickValue.x < -0.5f)
        {
            if (this.transform.localScale.x > 0.25f || transform.localScale.x < 2) 
                this.transform.localScale *= stickValue.x;
        }
        if (stickValue.y > 0.5f || stickValue.y < -0.5f)
        {
            var pos = this.transform.position;
            transform.position = new Vector3(pos.x, pos.y + stickValue.y * speed, pos.z);
        }
    }
}
