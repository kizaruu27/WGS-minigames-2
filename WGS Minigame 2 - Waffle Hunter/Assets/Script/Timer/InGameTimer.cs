using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class InGameTimer : MonoBehaviour
{
    public static InGameTimer instance;
    public float duration;
    public Text timerText;
    public bool timerIsPlay = false;
    string stringTimer = "00:00";
    PhotonView pv;
    // public GameObject GameOverUI;

    private void Awake()
    {
        instance = this;
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (timerIsPlay)
        {
            stringTimer = TimeCalculation();

            if (PhotonNetwork.IsMasterClient)
                pv.RPC(nameof(TimeSync), RpcTarget.Others, stringTimer);

            timerText.text = stringTimer;

            if (duration <= 0)
            {
                duration = 0;
                timerText.text = "00:00";
                // GameOverUI.SetActive(true);
                // Time.timeScale = 0;
            }
        }
    }

    string TimeCalculation()
    {
        duration -= Time.deltaTime;

        float minutes = Mathf.FloorToInt((duration / 60));
        float seconds = Mathf.FloorToInt((duration % 60));

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    [PunRPC] void TimeSync(string syncTimer) => stringTimer = syncTimer;
}
