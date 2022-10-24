using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using RunMinigames.Manager.Lobby;

public class M2_RoomItem : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    public RoomInfo roomInfo;
    M2_LobbyManager manager;
    bool isRoomExist;

    private void Start()
    {
        manager = FindObjectOfType<M2_LobbyManager>();
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void checkRoomIsExist(bool status)
    {
        isRoomExist = status;
    }

    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);

    }
}
