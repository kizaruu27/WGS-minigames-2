using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

namespace RunMinigames.Manager.Room
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        public static RoomManager instance;
        bool GameStart = false;
        public Button readyButton;

        [Header("Timer Components")]
        public TextMeshProUGUI TextTimer;
        public double CooldownTime = 30;
        private double Timer;
        [SerializeField] bool startTimer = false;
        double startTime;
        ExitGames.Client.Photon.Hashtable CustomeValue = new ExitGames.Client.Photon.Hashtable();
        public int playerReadyCount;

        [Header("Choose Avatar Components")]
        public GameObject DisplayAvaParent;
        public ToggleGroup AvaToggleGroup;

        [Header("Scenes")]
        public string[] sceneName;

        private void Awake() => instance = this;

        void SetText(double _timer)
        {
            TextTimer.text = _timer.ToString("0");
        }

        private void Update()
        {
            if (GameStart == false)
            {
                StartCountDown();

                if (PhotonNetwork.LocalPlayer.IsMasterClient)
                {
                    if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
                    {
                        startTimer = false;
                        SetText(CooldownTime);
                    }
                    else if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && startTimer == false)
                    {
                        SetStartTime();
                    }

                    if (playerReadyCount >= PhotonNetwork.CurrentRoom.PlayerCount)
                    {
                        playerReadyCount = 0;
                    }
                    else if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && playerReadyCount == PhotonNetwork.CurrentRoom.PlayerCount)
                        StartGame();
                }
                else
                {
                    SetStartTimeClient();
                }
            }
        }

        void SetStartTime()
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                startTime = PhotonNetwork.Time;
                startTimer = true;

                if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
                {
                    CustomeValue["StartTime"] = startTime;
                    PhotonNetwork.LocalPlayer.CustomProperties = CustomeValue;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
                }
                else
                {
                    CustomeValue.Add("StartTime", startTime);
                    PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
                }
            }
        }

        void SetStartTimeClient()
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
            {
                startTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
                startTimer = true;
            }
        }

        void StartCountDown()
        {
            if (startTimer == false)
                return;

            Timer = CooldownTime - (PhotonNetwork.Time - startTime);
            SetText(Timer);

            if (Timer < 0)
            {
                SetText(0);
                startTimer = false;

                if (PhotonNetwork.IsMasterClient)
                    StartGame();
            }
        }
        public void StartGame()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            GameStart = true;
            PhotonNetwork.LoadLevel(sceneName[Random.Range(0, sceneName.Length)]);
        }

        public void SetPlayerReady()
        {
            if (PhotonNetwork.LocalPlayer.IsLocal)
            {
                readyButton.interactable = false;
                photonView.RPC("RPC_SetReadyState", RpcTarget.AllBufferedViaServer, true);
            }
        }

        [PunRPC]
        public void RPC_SetReadyState(bool add = true)
        {
            if (add)
            {
                RoomManager.instance.playerReadyCount++;
            }
            else
            {
                RoomManager.instance.playerReadyCount--;
            }
        }
    }
}
