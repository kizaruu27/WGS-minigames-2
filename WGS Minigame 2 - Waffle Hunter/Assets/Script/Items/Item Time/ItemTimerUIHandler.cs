using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTimerUIHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject UITimer;
    [SerializeField] Text textMessage, textTimer;
    [SerializeField] string message;
    [SerializeField] float time;
    public bool isActive;


    void Update()
    {
       if (isActive)
       {
           ActivateUITimer();
       }

        if (time <= 0)
        {
            DeactivateTimer();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag ==  "Direction")
        {
            isActive = true;
            time = GetComponent<DirectionHolder>().itemTime;
            message = "Direction Activated";
        }

        if (col.tag == "Shield")
        {
            isActive = true;
            time = GetComponent<ShieldHandler>().shieldTime;
            message = "Shield Activated";
        }

        if (col.tag == "SpeedChange")
        {
            isActive = true;
            time = col.GetComponent<SpeedUpItem>().itemTime;
            message = "Speed Up!";
        }
    }

    void ActivateUITimer()
    {

        UITimer.SetActive(true);
    
        time -= Time.deltaTime;
        float seconds = Mathf.FloorToInt(time % 60);
        textTimer.text = seconds.ToString();
        

        UITimer.SetActive(true);
        textMessage.text = message;
    }

    void DeactivateTimer()
    {
        time = 0;
        UITimer.SetActive(false);
        isActive = false;
    }
}
