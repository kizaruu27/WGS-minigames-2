using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class M2_WaffleHunterSpawner : MonoBehaviour
{
    public GameObject Item;
    public GameObject[] playerPrefabs;
    public int PlayerNow;

    [Header("Spawn Point")]
    public Transform[] ItemSpawnPoints;
    public Transform[] playerSpawnPoints;
    int itemSpawnIndex;
    PhotonView view;

    private void Awake()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;

        Transform spawnPoint = playerSpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        GameObject playerSpawn = playerPrefabs[PlayerPrefs.GetInt("playerAvatar")];
        PhotonNetwork.Instantiate(playerSpawn.name, spawnPoint.position, Quaternion.identity);

        PlayerNow = PhotonNetwork.CurrentRoom.PlayerCount;
        view = GetComponent<PhotonView>();

        itemSpawnIndex = Random.Range(PlayerNow + 1, ItemSpawnPoints.Length);

    }

    // Start is called before the first frame update
    void Start()
    {

        view.RPC("SpawnPointWaffle", RpcTarget.OthersBuffered, itemSpawnIndex);

        // int playerSpawnIndex = Random.Range(0, playerSpawnPoints.Length);

        // if (itemSpawnIndex == playerSpawnIndex)
        // {
        //     if (itemSpawnIndex > ItemSpawnPoints.Length - 2)
        //     {
        //         itemSpawnIndex = 0;
        //     }
        //     else
        //     {
        //         itemSpawnIndex += 2;
        //     }
        // }

        // Instantiate(Item, ItemSpawnPoints[itemSpawnIndex].position, transform.rotation);
        // Instantiate(Player, playerSpawnPoints[playerSpawnIndex].position, transform.rotation);

        // Instantiate(Item, ItemSpawnPoints[itemSpawnIndex].position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    void SpawnPointWaffle(int locationIndex) => itemSpawnIndex = locationIndex;

}
