using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] PlayerPrefabs;
    public Transform[] SpawnPoints;

    private void Awake()
    {
        // Transform spawnPoint = SpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        Transform spawnPoint = SpawnPoints[PlayerPrefs.GetInt("positionIndex")];
        GameObject playerAvatar = PlayerPrefabs[PlayerPrefs.GetInt("playerAvatar")];
        PhotonNetwork.Instantiate(playerAvatar.name, spawnPoint.position, Quaternion.identity);
    }

}
