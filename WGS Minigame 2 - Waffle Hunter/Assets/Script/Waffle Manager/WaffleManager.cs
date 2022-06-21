using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleManager : MonoBehaviour
{
    public ScriptableValue waffleValue;
    public int waffleCollected;
    public GameObject WinUI;
    public Text waffleText;


    private void Start()
    {
        waffleValue.value = 0;
        waffleValue.value = waffleCollected;
    }

    private void Update()
    {
        waffleText.text = "Waffle Collected: " + waffleCollected.ToString();
        WinGame();
    }

    void WinGame()
    {
        if (waffleCollected == 10)
        {
            // Time.timeScale = 0;
            WinUI.SetActive(true);
        }
    }
}
