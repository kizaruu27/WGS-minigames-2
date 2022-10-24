using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using RoyaleMinigames.View.ChooseAvatar;
using TMPro;
using UnityEngine.UI;


namespace RoyaleMinigames.Manager.Lobby
{
    public class M2_LobbyManager : MonoBehaviourPunCallbacks
    {
        [Header("Canvas")]
        [SerializeField] Canvas canvas;

        [Header("Lobby")]
        public GameObject lobbyPanel;
        public GameObject menuUI;
        public GameObject searchPlayerPanel;

        [Header("Room")]
        public TMP_InputField roomInputField;
        public GameObject roomPanel;
        public TextMeshProUGUI roomName;

        public M2_RoomItem m2RoomItemPrefab;
        List<M2_RoomItem> roomItemList = new List<M2_RoomItem>();

        [Header("Player")]
        public List<M2_PlayerItem> playerItemsList = new List<M2_PlayerItem>();
        public M2_PlayerItem m2PlayerItemPrefab;
        public Transform playerItemParent;



        [Header("Modal")]
        public TextMeshProUGUI modalTitle;
        public TextMeshProUGUI modalMessage;
        public Button closeModal;
        public GameObject modalPanel;


        [Header("Loading")]
        [SerializeField] GameObject loadingPanel;


        [Header("Utilities")]
        public GameObject playButton;
        public Transform contentObject;
        public float timeBetweenUpdates = 1.5f;
        float nextUpdateTime;

        [Header("Scenes")]
        public string[] sceneName;

        [Header("Room Index")] 
        [SerializeField] private int roomIndex;

        [SerializeField] private bool isInRoom;



        private void Start()
        {
            PhotonNetwork.JoinLobby();
            modalPanel.SetActive(false);
            PhotonNetwork.OfflineMode = false;
            isInRoom = false;

            StartCoroutine(loadRoom());
        }

        public void onClickMatchmaking()
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 4;
            
            Debug.Log(PhotonNetwork.CountOfPlayersInRooms);

            if (PhotonNetwork.CountOfPlayersInRooms < roomOptions.MaxPlayers)
            {
                Debug.Log("Masuk room");
                PhotonNetwork.JoinOrCreateRoom("Room " + roomIndex.ToString(), roomOptions, TypedLobby.Default);
                isInRoom = true;
            }
            else
            {
                PhotonNetwork.LeaveRoom();
                roomIndex++;
                PhotonNetwork.JoinOrCreateRoom("Room " + roomIndex.ToString(), roomOptions, TypedLobby.Default);
                isInRoom = true;
            }
        }
        
        public override void OnJoinedRoom()
        {
            loadingPanel.SetActive(false);
            lobbyPanel.SetActive(false);
            
            searchPlayerPanel.SetActive(true);
            roomName.text = PhotonNetwork.CurrentRoom.Name;
            UpdatePlayerList();
        }

        IEnumerator loadRoom()
        {
            yield return new WaitForSeconds(3);

            if (isInRoom)
            {
                yield return new WaitForSeconds(3);
                //to room
                GoToRoom();
            }
            else
            {
                StartCoroutine(loadRoom());
            }
        }

        void GoToRoom()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                // activate room panel
                ActivateRoomPanel();
            }
        }

        void ActivateRoomPanel()
        {
            roomPanel.SetActive(true);
            searchPlayerPanel.SetActive(false);
        }


        public void OnClickCreate()
        {
            if (roomInputField.text.Length >= 1)
            {
                loadingPanel.SetActive(true);

                if (PhotonNetwork.IsConnected)
                {
                    PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 4, BroadcastPropsChangeToAll = true });
                }
                else
                {
                    Modal("Not Connected", "Please Check Your Internet Connection");

                }
            }
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Modal("Failed To Create Room", message);
        }


        public void OnCloseModal()
        {
            modalPanel.SetActive(false);
        }

        public void Modal(string title, string message)
        {
            modalPanel.SetActive(true);
            loadingPanel.SetActive(false);
            modalTitle.text = title;
            modalMessage.text = message;
        }

     
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Modal("Failed To Join Room", message);

            if (message.ToLower() == "game does not exist")
            {
                foreach (M2_RoomItem item in roomItemList)
                {
                    Destroy(item.gameObject);
                }
                roomItemList.Clear();
            }
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            if (Time.time >= nextUpdateTime)
            {
                UpdateRoomList(roomList);
                nextUpdateTime = Time.time + timeBetweenUpdates;
            }
        }

        void UpdateRoomList(List<RoomInfo> list)
        {
            foreach (M2_RoomItem item in roomItemList)
            {
                if (item.gameObject.name != null)
                    Destroy(item.gameObject);
            }
            roomItemList.Clear();

            foreach (RoomInfo room in list)
            {
                M2_RoomItem newM2Room = Instantiate(m2RoomItemPrefab, contentObject);
                newM2Room.SetRoomName(room.Name);
                newM2Room.roomInfo = room;

                if (!room.IsOpen || room.RemovedFromList)
                {
                    roomItemList.Remove(newM2Room);
                }

                roomItemList.Add(newM2Room);

            }
        }

        public void JoinRoom(string roomName)
        {
            loadingPanel.SetActive(true);
            PhotonNetwork.JoinRoom(roomName);
        }

        public void OnClickLeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            roomPanel.SetActive(false);
            lobbyPanel.SetActive(true);
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        void UpdatePlayerList()
        {
            foreach (M2_PlayerItem item in playerItemsList)
            {
                Destroy(item.gameObject);
            }
            playerItemsList.Clear();

            if (PhotonNetwork.CurrentRoom == null)
            {
                return;
            }

            foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                M2_PlayerItem newM2PlayerItem = Instantiate(m2PlayerItemPrefab, playerItemParent);  // call the player item
                newM2PlayerItem.SetPlayerInfo(player.Value);

                if (player.Value == PhotonNetwork.LocalPlayer)
                {
                    newM2PlayerItem.ApplyLocalChanges();
                }

                playerItemsList.Add(newM2PlayerItem);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdatePlayerList();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayerList();
        }

        public void OnClickModalDisconnect()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable || !PhotonNetwork.IsConnected)
                OnClickDisconnect();
        }

        private void Update()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable || !PhotonNetwork.IsConnected)
            {
                Modal("Connection Error", " Check internet connection!");
            }

            playButton.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1);
        }

        public void OnClickPlayButton()
        {
            PhotonNetwork.LoadLevel(sceneName[Random.Range(0, sceneName.Length)]);
            // PhotonNetwork.LoadLevel(sceneName[1]); -> buat testing
        }

        public void OnClickDisconnect()
        {
            PhotonNetwork.Disconnect();
        }

    }
}
