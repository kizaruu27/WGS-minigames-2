using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WaffleHandler : MonoBehaviour
{
    PhotonView pv;

    public float waffle; //ini waffle
    ShieldHandler handler;

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
    }


    private void Update() 
    {
        if (waffle >= 10)
        {
            Time.timeScale = 0;
            //MenuUI.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Waffle")
        {
            IncreaseWaffle();
        }
    }

    void IncreaseWaffle()
    {
        if (pv.IsMine)
        {
            waffle++;
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

    void DisableWaffleMessage()
    {
        waffleMessage.text = null;
    }
}
