using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NpcSpawner : MonoBehaviour
{
    public GameObject NpcCharacter;
    [SerializeField] Transform[] SpawnPoints;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int spawnPoint = Random.Range(0, SpawnPoints.Length);
            PhotonNetwork.Instantiate(NpcCharacter.name, SpawnPoints[spawnPoint].position, Quaternion.identity);
            Debug.Log($"is master client name {NpcCharacter.name}, spawn point: {spawnPoint}");
        }
    }
}
