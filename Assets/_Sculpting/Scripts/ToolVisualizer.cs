using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class ToolVisualizer : MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;

        public void SetMaterial(Material mat)
        {
            renderer.sharedMaterial = mat;
        }

        public void SetSize(float size)
        {
            transform.localScale = Vector3.one * size;
        }
    }
}
