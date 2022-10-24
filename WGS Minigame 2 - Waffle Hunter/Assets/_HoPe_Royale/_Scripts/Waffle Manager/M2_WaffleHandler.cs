using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class M2_WaffleHandler : MonoBehaviour
{
    PhotonView pv;

    public float waffle = 0; //ini waffle
    M2_ShieldHandler handler;

    [Header("Validation")]
    public bool isWin;

    [Header("UI Component")]
    [SerializeField] Text waffleTextUI;
    [SerializeField] GameObject MenuUI;
    [SerializeField] M2_UIAnimationHandler uIAnimationHandler;
    [SerializeField] Text waffleMessage;

    // PodiumManager pm;
    M2_ScoreManager _m2ScoreManager;

    private void Awake()
    {
        handler = GetComponent<M2_ShieldHandler>();
        pv = GetComponent<PhotonView>();

        _m2ScoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<M2_ScoreManager>();

    }

    private void Start()
    {
        //waffle = 0;
        uIAnimationHandler = GameObject.FindGameObjectWithTag("Notification").GetComponent<M2_UIAnimationHandler>();
        waffleTextUI = GameObject.FindGameObjectWithTag("Waffle Text").GetComponent<Text>();
        waffleMessage = GameObject.FindGameObjectWithTag("Notification Text").GetComponent<Text>();
        // pm = GameObject.FindGameObjectWithTag("Finish UI").GetComponent<PodiumManager>();


        pv.RPC("RPC_SetPlayersData", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, PhotonNetwork.LocalPlayer.NickName, waffle);

        if (pv.IsMine)
        {
            waffleTextUI.text = waffle.ToString();
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

    private void FixedUpdate()
    {

        // pv.RPC("RPC_SetPlayerScore", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber - 1, waffle);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waffle")
        {
            IncreaseWaffle();
        }
        if (other.tag == "Hit")
        {
            if (GetComponent<M2_ShieldHandler>().shieldActivated)
            {
                GetComponent<M2_ShieldHandler>().shieldActivated = false;
            }
            else
            {
                DecreaseWaffle();
            }
        }

        if (pv.IsMine)
            pv.RPC("RPC_SetPlayerScore", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, waffle);
    }

    void IncreaseWaffle()
    {
        if (pv.IsMine)
        {
            M2_ObjectAudioManager.instance.PlayWaffleAudio();
            waffle++;
            waffleTextUI.text = waffle.ToString();
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
            waffleTextUI.text = waffle.ToString();
        }
    }
    public void GameIsDone()
    {
        if (M2_InGameTimer.instance.duration == 0 || M2_GameFlowManager.instance.isDone && pv.IsMine)
        {
            pv.RPC(nameof(RPC_SendToPodium), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1, waffle, PhotonNetwork.LocalPlayer.NickName);
        }
    }

    [PunRPC]
    void RPC_SendToPodium(int id, float score, string nickname)
    {
        // pm.Finish(id, score, nickname);
    }

    [PunRPC]
    void Test()
    {
        Debug.Log(pv);
    }

    [PunRPC]
    void RPC_SetPlayersData(int _playerId, string _playerName, float _playerScore)
    {
        _m2ScoreManager.AddPlayerData(_playerId, _playerName, _playerScore);
    }

    [PunRPC]
    void RPC_SetPlayerScore(int _playerId, float _playerScore)
    {
        _m2ScoreManager.SetPlayerScore(_playerId, _playerScore);
    }
}
