using System.Collections;
using System.Collections.Generic;
using Paint;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    void Update(){

        bool click;
        click = mouseSingleClick ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);

        if (click)
        {
            Vector3 position = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f)){
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                transform.position = hit.point;
                ToolTarget2D p = hit.collider.GetComponent<ToolTarget2D>();
                if(p != null){
                    PaintGameManager.Instance.Paint(p, hit.point, radius, hardness, strength, paintColor);
                }
            }
        }

    }


}
