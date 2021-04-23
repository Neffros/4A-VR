using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Core;
using Sirenix.Utilities;
using UnityEngine;

public class ToolTarget2D : ToolTarget
{
    public Renderer renderer;
    public List<LayerMask> layers;
    public Material targetMaterial;
    private static readonly int AppliedColor = Shader.PropertyToID("_appliedColor");
    private static readonly int Positions = Shader.PropertyToID("_positions");



    private void OnCollisionStay(Collision other)
    {
        Debug.Log("collision");
        foreach (var layer in layers)
        {
            if ((layer & 1 << other.gameObject.layer) != 0)
            {
                other.gameObject.GetComponent<Brush2D>().ApplyEffect(this);
                Color color = Color.black;
                Shader.SetGlobalColor(AppliedColor, color);
                 
               
                Vector4[] contactPositions = new Vector4[other.contactCount];
                //Vector3[] co = new Vector3[other.contactCount];
                foreach (var contact in other.contacts)
                {
                    contactPositions.Append(new Vector4(contact.point.x, contact.point.y, contact.point.z, 0));
                    Debug.Log("point:" + contact.point);
                }
                targetMaterial.SetVectorArray(Positions, contactPositions);
            
                renderer.material = targetMaterial;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
