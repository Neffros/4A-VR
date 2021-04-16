using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private InputDevice leftController;
    public ControllerDict brushDict;
    private Transform controllerTransform;

    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = GameManager.Instance.GameData;
        gameData.controllerManager.ChangeController(brushDict);
        leftController = gameData.controllerManager.Controllers[0];
    }

    // Update is called once per frame
    void Update()
    {
        /*leftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
        {
            
        }*/
    }
}
