using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public Dropdown mainHand;
    public Dropdown playMode;

    public void StartGame()
    {
        SceneManager.LoadScene(3); //TODO change to definitive
    }

    public void DisplaySettings()
    {
        settingsPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitSettings()
    {
        GameManager gameManager = GameManager.Instance;
        if (mainHand.value == 0)
            gameManager.GameData.LeftHand = false;
        else
            gameManager.GameData.LeftHand = true;

        if (playMode.value == 0)
            gameManager.GameData.Seated = false;
        else
            gameManager.GameData.Seated = true;
        
        settingsPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);


    }

}
//test