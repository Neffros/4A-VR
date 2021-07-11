using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class DrawLineMeshManager : Tool
    {
        [SerializeField] private float lineSize = 0.03f;
        [SerializeField] private Material material;
        
        private GraphicsLineRenderer currentLine;
        
        public override void OnRightTriggerBegin()
        {
            base.OnRightTriggerBegin();
            var go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            currentLine = go.AddComponent<GraphicsLineRenderer>();

            currentLine.Init(lineSize, material);
        }

        public override void OnRightTrigger()
        {
            base.OnRightTrigger();
            currentLine.AddPoint(RightHand.transform.position);
        }
    }
}
