using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    

    public void StartGame()
    {
        SceneManager.LoadScene(3); //TODO change to definitive
    }

    public void DisplaySettings()
    {
        settingsPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
//test