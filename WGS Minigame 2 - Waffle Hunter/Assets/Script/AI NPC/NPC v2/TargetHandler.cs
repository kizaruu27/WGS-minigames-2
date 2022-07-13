
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TargetHandler : MonoBehaviour
{
    [Header("Player Target List")]
    public List<GameObject> Players;

    [Header("Component")]
    [SerializeField] PhotonView photonView;
    [SerializeField] CheckPlayerConnected checkPlayer;

    private void Update() => StartCoroutine(checkPlayer.WaitAllPlayerReady(AddPlayer));

    public void AddPlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // get player game object
            if (Players.Count < (int)PhotonNetwork.PlayerList.Length)
            {
                foreach (var Player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Players.Add(Player);
                }
            }
        }
    }
}
