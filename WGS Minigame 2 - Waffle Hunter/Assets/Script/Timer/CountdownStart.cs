using UnityEngine;
using Photon.Pun;
using TMPro;

public class CountdownStart : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] TextMeshProUGUI _CountdownTXT;
    public float InitialCountdown = 4f;
    bool isStart;

    [Header("Timer")]
    [SerializeField] InGameTimer _inGameTimer;



    [Header("Component")]
    [SerializeField] PhotonView photonView;
    [SerializeField] CheckPlayerConnected checkPlayer;

    private void Update() => StartCoroutine(checkPlayer.WaitAllPlayerReady(StartCountdown));

    public void StartCountdown()
    {
        InitialCountdown -= Time.deltaTime;

        if (PhotonNetwork.IsMasterClient)
            photonView.RPC(nameof(SetCountdown), RpcTarget.Others, InitialCountdown);

        isStart = InitialCountdown <= 0f;

        _CountdownTXT.fontSize = InitialCountdown > 2 ? 200f : 80f;
        _CountdownTXT.text = InitialCountdown > 2 ? $"{(int)(InitialCountdown - 1)}" : "Start";

        gameObject.SetActive(!isStart);

        _inGameTimer.timerIsPlay = isStart;

        if (isStart)
            checkPlayer.ActivateController();
    }

    [PunRPC]
    void SetCountdown(float newCountdown) => InitialCountdown = newCountdown;
}