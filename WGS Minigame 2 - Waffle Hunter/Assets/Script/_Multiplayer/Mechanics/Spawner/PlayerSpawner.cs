using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] PlayerPrefabs;
    public Transform[] SpawnPoints;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        Transform spawnPoint = SpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        GameObject playerAvatar = PlayerPrefabs[PlayerPrefs.GetInt("playerAvatar")];
        PhotonNetwork.Instantiate(playerAvatar.name, spawnPoint.position, Quaternion.identity);
    }

}
