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

    [Header("Notification ELements")]
    [SerializeField] UIAnimationHandler uIAnimationHandler;
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
        SpeedUpIndicator = GameObject.FindGameObjectWithTag("Speed indicator").GetComponent<ItemsIndicatorHandler>();
        ShieldIndicator = GameObject.FindGameObjectWithTag("Shield Indicator").GetComponent<ItemsIndicatorHandler>();
        DirectionIndicator = GameObject.FindGameObjectWithTag("Direction Indicator").GetComponent<ItemsIndicatorHandler>();
        uIAnimationHandler = GameObject.FindGameObjectWithTag("Notification").GetComponent<UIAnimationHandler>();
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
            time = GetComponent<DirectionHolder>().itemTime;
            message = "Direction Acquired!";
            DirectionIndicator.activateIndicator();
            CallUINotif(message);
            // pv.RPC("CallUINotif", RpcTarget.AllBuffered, message);
        }

        if (col.tag == "Shield")
        {
            isActive = true;
            time = GetComponent<ShieldHandler>().shieldTime;
            message = "Shield Acquired!";
            ShieldIndicator.activateIndicator();
            CallUINotif(message);
            // pv.RPC("CallUINotif", RpcTarget.AllBuffered, message);
        }

        if (col.tag == "SpeedChange")
        {
            isActive = true;
            time = col.GetComponent<SpeedUpItem>().itemTime;
            message = "Speed Up!";
            SpeedUpIndicator.activateIndicator();
            CallUINotif(message);
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
            DirectionIndicator.deactivateIndicator();
            SpeedUpIndicator.deactivateIndicator();
            ShieldIndicator.deactivateIndicator();
        }
    }


    void CallUINotif(string notif)
    {
        if (pv.IsMine)
        {
            uIAnimationHandler.PlayNotifAnimation();
            notificationText.text = notif;
        }
    }
}
