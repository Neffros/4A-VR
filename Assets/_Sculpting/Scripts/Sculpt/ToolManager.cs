using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class ToolManager : MonoBehaviour
    {
        [SerializeField] private List<Tool> tools;

        private int _currentToolIndex;

        public Tool CurrentTool => tools[_currentToolIndex];

        private void Awake()
        {
            EnableTool(0);
        }

        public void EnableTool(int index)
        {
            _currentToolIndex = index;

            for (var j = 0; j < tools.Count; j++)
            {
                tools[j].Enable(j == index);
            }
        }
    }
}
