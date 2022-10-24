using UnityEngine;
using Photon.Pun;
using TMPro;

public class M2_CountdownStart : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] TextMeshProUGUI _CountdownTXT;
    public float InitialCountdown = 4f;
    bool isStart;

    [Header("Timer")]
    [SerializeField] M2_InGameTimer m2InGameTimer;



    [Header("Component")]
    [SerializeField] PhotonView photonView;
    [SerializeField] M2_CheckPlayerConnected m2CheckPlayer;

    private void Update() => StartCoroutine(m2CheckPlayer.WaitAllPlayerReady(StartCountdown));

    public void StartCountdown()
    {
        InitialCountdown -= Time.deltaTime;

        if (PhotonNetwork.IsMasterClient)
            photonView.RPC(nameof(SetCountdown), RpcTarget.Others, InitialCountdown);

        isStart = InitialCountdown <= 0f;

        _CountdownTXT.fontSize = InitialCountdown > 2 ? 200f : 80f;
        _CountdownTXT.text = InitialCountdown > 2 ? $"{(int)(InitialCountdown - 1)}" : "Start";

        gameObject.SetActive(!isStart);

        m2InGameTimer.timerIsPlay = isStart;

        if (isStart)
            m2CheckPlayer.ActivateController();
    }

    [PunRPC]
    void SetCountdown(float newCountdown) => InitialCountdown = newCountdown;
}