using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class M2_ItemTimerUIHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject UITimer;
    [SerializeField] Text textMessage, textTimer;
    [SerializeField] string message;
    [SerializeField] float time;

    [Header("Notification ELements")]
    [SerializeField] Text notificationText;

    [Header("Message Elements")]
    [SerializeField] Text messageText;
    Text messageNotification;

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
        notificationText = GameObject.FindGameObjectWithTag("Notification Text").GetComponent<Text>();
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
        if (col.tag == "Direction")
        {
            isActive = true;
            time = GetComponent<M2_DirectionHolder>().itemTime;
            message = "Direction Acquired!";
            CallUINotif(message);
            
            M2_ItemsIndicatorHandler DirectionIndicator = GameObject.Find("Direction Indicator").GetComponent<M2_ItemsIndicatorHandler>();
            DirectionIndicator.activateIndicator();
            // pv.RPC("CallUINotif", RpcTarget.AllBuffered, message);
        }

        if (col.tag == "Shield")
        {
            isActive = true;
            time = GetComponent<M2_ShieldHandler>().shieldTime;
            message = "Shield Acquired!";
            CallUINotif(message);
            
            M2_ItemsIndicatorHandler ShieldIndicator = GameObject.Find("Shield Indicator").GetComponent<M2_ItemsIndicatorHandler>();
            ShieldIndicator.activateIndicator();
            // pv.RPC("CallUINotif", RpcTarget.AllBuffered, message);
        }

        if (col.tag == "SpeedChange")
        {
            isActive = true;
            time = col.GetComponent<M2_SpeedUpItem>().itemTime;
            message = "Speed Up!";
            CallUINotif(message);
            
            M2_ItemsIndicatorHandler SpeedUpIndicator = GameObject.Find("SpeedUp Indicator").GetComponent<M2_ItemsIndicatorHandler>();
            SpeedUpIndicator.activateIndicator();
            // pv.RPC("CallUINotif", RpcTarget.AllBuffered, message);

        }
    }

    void ActivateUITimer()
    {
        if (pv.IsMine)
        {
            time -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(time % 60);
            notificationText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z));
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
            M2_ItemsIndicatorHandler DirectionIndicator = GameObject.Find("Direction Indicator").GetComponent<M2_ItemsIndicatorHandler>();
            DirectionIndicator.deactivateIndicator();
            
            M2_ItemsIndicatorHandler SpeedUpIndicator = GameObject.Find("SpeedUp Indicator").GetComponent<M2_ItemsIndicatorHandler>();
            SpeedUpIndicator.deactivateIndicator();
            
            M2_ItemsIndicatorHandler ShieldIndicator = GameObject.Find("Shield Indicator").GetComponent<M2_ItemsIndicatorHandler>();
            ShieldIndicator.deactivateIndicator();
        }
    }


    void CallUINotif(string notif)
    {
        if (pv.IsMine)
        {
            M2_UIAnimationHandler uIAnimationHandler = GameObject.Find("UI Animation Handler").GetComponent<M2_UIAnimationHandler>();
            uIAnimationHandler.PlayNotifAnimation();
            notificationText.text = notif;
        }
    }
}
