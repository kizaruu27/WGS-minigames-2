using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameTimer : MonoBehaviour
{
    public float timer;
    public Text timerText;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        float minutes = Mathf.FloorToInt((timer / 60));
        float seconds = Mathf.FloorToInt((timer % 60));

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timer <= 0)
        {
            timer = 0;
            timerText.text = "00:00";
        }
    }
}
