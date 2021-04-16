using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace paint
{

    public class PaintControllerBehavior : MonoBehaviour
    {

        private InputDevice brush;
        private InputDevice leftController;
        public ControllerDict brushDict;
        private Transform controllerTransform;

        public GameObject cube;

        private GameData gameData;
        // Start is called before the first frame update
        void Start()
        {
            gameData = GameManager.Instance.GameData;
            gameData.controllerManager.ChangeController(brushDict);
            brush = gameData.controllerManager.Controllers[1];
            leftController = gameData.controllerManager.Controllers[0];
            //gameData.LeftHand ? gameData.controllerManager.Controllers[0] : gameData.controllerManager.Controllers[1]; //TODO CHANGE BASED ON MAIN HAND SETTING

        }

        // Update is called once per frame
        void Update()
        {

            
            brush.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            if (triggerValue > 0.1f)
            {
                cube.transform.localScale = new Vector3(triggerValue/10, triggerValue/10, triggerValue/10);
                Vector3 curPos = gameData.controllerManager.XRControllers[1].transform.position;
                Quaternion curRot = gameData.controllerManager.XRControllers[1].transform.rotation;
                Instantiate(cube, curPos, curRot);
            }
        }
    }

}