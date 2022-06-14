using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WaffleHandler : MonoBehaviour, IDemageable
{
    PhotonView pv;

    public float waffle; //ini waffle
    ShieldHandler handler;

    [Header("Validation")]
    public bool isWin;

    [Header("UI Component")]
    [SerializeField] Text waffleTextUI;
    [SerializeField] GameObject MenuUI;
    [SerializeField] Text waffleMessage;

    private void Awake()
    {
        handler = GetComponent<ShieldHandler>();
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        //waffle = 0;
        waffleTextUI = GameObject.FindGameObjectWithTag("Waffle Text").GetComponent<Text>();
        waffleMessage = GameObject.FindGameObjectWithTag("Waffle Message").GetComponent<Text>();

        if (pv.IsMine)
        {
            waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
        }

        isWin = false;
    }


    private void Update()
    {
        if (waffle >= 10)
        {
            Time.timeScale = 0;
            isWin = true;
            //MenuUI.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Waffle")
        {
            // IncreaseWaffle();
            pv.RPC("RPC_IncreaseWaffle", RpcTarget.All);
        }
    }

    void DisableWaffleMessage()
    {
        waffleMessage.text = null;
    }

    void IncreaseWaffle()
    {
        if (pv.IsMine)
        {
            // waffle++;
            waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
            waffleMessage.text = "Waffle Collected!";

            Invoke("DisableWaffleMessage", 1);
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
    void RPC_IncreaseWaffle()
    {
        if (!pv.IsMine) return;

        waffle++;
        waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
        waffleMessage.text = "Waffle Collected!";

        Invoke("DisableWaffleMessage", 1);
    }
}
