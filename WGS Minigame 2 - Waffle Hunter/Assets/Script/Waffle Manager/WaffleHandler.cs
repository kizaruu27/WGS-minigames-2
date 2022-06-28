using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WaffleHandler : MonoBehaviour, IDemageable
{
    PhotonView pv;

    public float waffle = 0; //ini waffle
    ShieldHandler handler;

    [Header("Validation")]
    public bool isWin;

    [Header("UI Component")]
    [SerializeField] Text waffleTextUI;
    [SerializeField] GameObject MenuUI;
    [SerializeField] UIAnimationHandler uIAnimationHandler;
    [SerializeField] Text waffleMessage;

    PodiumManager pm;

    private void Awake()
    {
        handler = GetComponent<ShieldHandler>();
        pv = GetComponent<PhotonView>();

    }

    private void Start()
    {
        //waffle = 0;
        uIAnimationHandler = GameObject.FindGameObjectWithTag("Notification").GetComponent<UIAnimationHandler>();
        waffleTextUI = GameObject.FindGameObjectWithTag("Waffle Text").GetComponent<Text>();
        waffleMessage = GameObject.FindGameObjectWithTag("Notification Text").GetComponent<Text>();
        pm = GameObject.FindGameObjectWithTag("Finish UI").GetComponent<PodiumManager>();


        if (pv.IsMine)
        {
            waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
        }

        isWin = false;
    }


    private void Update()
    {
        isWin = waffle >= 10;

        // if (waffle >= 10)
        // {
        //     // Time.timeScale = 0;
        //     // MenuUI.SetActive(true);
        // }

        if (isWin)
        {
            pv.RPC(nameof(Test), RpcTarget.All);
        }



        GameIsDone();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waffle")
        {
            IncreaseWaffle();
            // pv.RPC("RPC_IncreaseWaffle", RpcTarget.All);
        }
        if (other.tag == "Hit")
        {
            DecreaseWaffle();
        }
    }

    void IncreaseWaffle()
    {
        if (pv.IsMine)
        {
            waffle++;
            waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
            uIAnimationHandler.PlayNotifAnimation();
            waffleMessage.text = "Waffle Collected!";

            // Invoke("DisableWaffleMessage", 1);
        }
    }

    public void DecreaseWaffle()
    {
        if (pv.IsMine)
        {
            waffle--;
            if (waffle <= 0)
            {
                waffle = 0;
            }
            waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
        }
    }
    public void GotAttact() => pv.RPC("RPC_GotAttact", RpcTarget.All);

    public void GameIsDone()
    {
        if (InGameTimer.instance.duration == 0 || GameFlowManager.instance.isDone && pv.IsMine)
        {
            pv.RPC(nameof(RPC_SendToPodium), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1, waffle, PhotonNetwork.LocalPlayer.NickName);
        }
    }

    [PunRPC]
    void RPC_SendToPodium(int id, float score, string nickname)
    {
        pm.Finish(id, score, nickname);
    }

    [PunRPC]
    void RPC_GotAttact()
    {
        if (!pv.IsMine) return;

        Debug.Log("other palyer got damage:  " + PhotonNetwork.LocalPlayer.NickName);

        waffle--;
        if (waffle <= 0)
        {
            waffle = 0;
        }

        waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
    }

    [PunRPC]
    void Test()
    {
        Debug.Log(pv);
    }
}
