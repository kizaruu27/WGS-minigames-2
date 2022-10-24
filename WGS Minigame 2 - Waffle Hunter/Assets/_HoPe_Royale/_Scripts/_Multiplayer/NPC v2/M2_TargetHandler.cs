
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// -> For NPC Target
public class M2_TargetHandler : MonoBehaviour
{
    [Header("Player Target List")]
    public List<GameObject> Players;

    [Header("Component")]
    [SerializeField] PhotonView photonView;
    [SerializeField] M2_CheckPlayerConnected m2CheckPlayer;

    private void Update() => StartCoroutine(m2CheckPlayer.WaitAllPlayerReady(AddPlayer));

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
