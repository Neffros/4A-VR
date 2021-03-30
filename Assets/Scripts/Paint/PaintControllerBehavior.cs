using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace paint
{

    public class PaintControllerBehavior : MonoBehaviour
    {

        private InputDevice brush;
        public Transform rightControllerTransform;

        public GameObject cube;

        // Start is called before the first frame update
        void Start()
        {
            brush = GameManager.Instance.Device;
        }

        // Update is called once per frame
        void Update()
        {
            brush.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            if (triggerValue > 0.1f)
            {
                cube.transform.localScale = new Vector3(triggerValue, triggerValue, triggerValue);
                Instantiate(cube, rightControllerTransform);
            }
        }
    }

}