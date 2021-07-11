using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class DrawLineMeshManager : Tool
    {
        private float lineSize = 0.03f;
        [SerializeField] private Material material;
        [SerializeField] private Slider sizeSlider;
        [SerializeField] private ToolVisualizer toolVisualizer;

        [SerializeField] private Transform parent;
        
        private GraphicsLineRenderer currentLine;
        
        protected override void CustomUpdate()
        {
            base.CustomUpdate();
            lineSize = sizeSlider.value;
            toolVisualizer.SetSize(sizeSlider.value);
        }
        
        public override void OnRightGripBegin()
        {
            base.OnRightGripBegin();
            parent.SetParent(RightHand.transform, true);
        }

        public override void OnRightGripEnd()
        {
            base.OnRightGripEnd();
            parent.SetParent(null, true);
        }

        public override void OnRightTriggerBegin()
        {
            base.OnRightTriggerBegin();
            var go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            currentLine = go.AddComponent<GraphicsLineRenderer>();

            currentLine.Init(lineSize, material);
            
            go.transform.SetParent(parent, true);
        }

        public override void OnRightTrigger()
        {
            base.OnRightTrigger();
            currentLine.AddPoint(RightHand.transform.position);
        }

    }
}
