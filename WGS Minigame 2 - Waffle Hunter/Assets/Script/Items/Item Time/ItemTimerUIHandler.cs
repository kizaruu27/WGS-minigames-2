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
    bool isActive;


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           isActive = true;
        }
    }

    void ActivateUITimer()
    {
        time -= Time.deltaTime;

        float seconds = Mathf.FloorToInt(time % 60);

        UITimer.SetActive(true);
        textMessage.text = message;
        textTimer.text = seconds.ToString();
    }

    void DeactivateTimer()
    {
        time = 0;
        UITimer.SetActive(false);
        isActive = false;
    }
}
