using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class DrawLineRendererManager : Tool
    {
        private int _numberClicks;
        private LineRenderer currentLine;
        
        public override void OnRightTriggerBegin()
        {
            base.OnRightTriggerBegin();
            currentLine = new GameObject().AddComponent<LineRenderer>();
            currentLine.useWorldSpace = true;
            currentLine.startWidth = .1f;
            currentLine.endWidth = .1f;
            _numberClicks = 0;
        }

        public override void OnRightTrigger()
        {
            base.OnRightTrigger();
            currentLine.positionCount = _numberClicks + 1;
            currentLine.SetPosition(_numberClicks, RightHand.transform.position);
            _numberClicks++;
        }
    }
}
