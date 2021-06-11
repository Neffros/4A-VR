using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class DrawLineMeshManager : DrawLineManager
    {
        [SerializeField] private float lineSize = 0.03f;
        [SerializeField] private Material material;
        
        private GraphicsLineRenderer currentLine;
        
        protected override void OnRightTriggerBegin()
        {
            base.OnRightTriggerBegin();
            var go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            currentLine = go.AddComponent<GraphicsLineRenderer>();

            currentLine.Init(lineSize, material);
        }

        protected override void OnRightTrigger()
        {
            base.OnRightTrigger();
            currentLine.AddPoint(RightHand.transform.position);
        }
    }
}
