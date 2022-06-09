using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] PlayerPrefabs;
    public Transform[] SpawnPoints;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();


        Transform spawnPoint = SpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        // GameObject playerAvatar = PlayerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        GameObject playerAvatar = PlayerPrefabs[PlayerPrefs.GetInt("playerAvatar")];
        PhotonNetwork.Instantiate(playerAvatar.name, spawnPoint.position, Quaternion.identity);

        // foreach (Player player in PhotonNetwork.PlayerList)
        // {
        //     Debug.Log(player.NickName);
        // }

        Debug.Log((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]);
    }

}
