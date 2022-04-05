using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleManager : MonoBehaviour
{
    public ScriptableValue waffleValue;
    
    public Text waffleText;


    private void Start()
    {
        waffleValue.value = 0;
    }

    private void Update()
    {
        waffleText.text = "Waffle Collected: " + waffleValue.value.ToString();
    }

 
}
