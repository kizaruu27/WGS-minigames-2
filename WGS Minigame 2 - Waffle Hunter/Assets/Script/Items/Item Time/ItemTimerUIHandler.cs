using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ItemTimerUIHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject UITimer;
    [SerializeField] Text textMessage, textTimer;
    [SerializeField] string message;
    [SerializeField] float time;

    [Header("Indicator Elements")]
    [SerializeField] ItemsIndicatorHandler SpeedUpIndicator;
    [SerializeField] ItemsIndicatorHandler ShieldIndicator;
    [SerializeField] ItemsIndicatorHandler DirectionIndicator;

    public bool isActive;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start() 
    {
        //timer UI component
        UITimer = GameObject.FindGameObjectWithTag("Timer UI");
        textMessage = GameObject.FindGameObjectWithTag("Item Message").GetComponent<Text>();
        textTimer = GameObject.FindGameObjectWithTag("Item Time").GetComponent<Text>();   

        //indicator UI
        SpeedUpIndicator = GameObject.FindGameObjectWithTag("Speed indicator").GetComponent<ItemsIndicatorHandler>();
        ShieldIndicator = GameObject.FindGameObjectWithTag("Shield Indicator").GetComponent<ItemsIndicatorHandler>();
        DirectionIndicator = GameObject.FindGameObjectWithTag("Direction Indicator").GetComponent<ItemsIndicatorHandler>();
    }

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
            DirectionIndicator.activateIndicator();
        }

        if (col.tag == "Shield")
        {
            isActive = true;
            time = GetComponent<ShieldHandler>().shieldTime;
            message = "Shield Activated";
            ShieldIndicator.activateIndicator();
        }

        if (col.tag == "SpeedChange")
        {
            isActive = true;
            time = col.GetComponent<SpeedUpItem>().itemTime;
            message = "Speed Up!";
            SpeedUpIndicator.activateIndicator();
        }
    }

    void ActivateUITimer()
    {
        if (pv.IsMine)
        {
            //UITimer.SetActive(true);
        
            time -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(time % 60);
           // textTimer.text = seconds.ToString();
            

            //UITimer.SetActive(true);
           // textMessage.text = message;
        }
    }

    void DeactivateTimer()
    {
        if (pv.IsMine)
        {
            time = 0;
            //UITimer.SetActive(false);
            textMessage.text = null;
            textTimer.text = null;
            isActive = false;
            
            //deactivate indicator
            DirectionIndicator.deactivateIndicator();
            SpeedUpIndicator.deactivateIndicator();
            ShieldIndicator.deactivateIndicator();
        }
    }
}
