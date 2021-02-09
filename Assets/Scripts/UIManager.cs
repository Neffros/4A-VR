using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI highScoreTxt;
    public TextMeshProUGUI timerTxt;
    private void Start()
    {
        GameManager.Instance.UiManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        timerTxt.SetText(GameManager.Instance.GameData.Timer.ToString());
    }
}
