
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RunMinigames.Models;
using Photon.Pun;


public class PodiumManager : MonoBehaviour
{
    public static PodiumManager instance;

    [Header("Game Finish Condition")]
    [SerializeField] InGameTimer timer;
    WaffleHandler waffleHandler;


    [Header("Player List")]
    List<MPodium> playerFinishList = new List<MPodium>();

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    public void InitializePlayer(int id, string name, float score)
    {
        var playerFinish = new MPodium();
        playerFinish.name = name;
        playerFinish.score = score;
        playerFinish.id = id;

        playerFinishList.Add(playerFinish);
    }

    public IEnumerable<MPodium> GetLeaderboardData() => playerFinishList.OrderByDescending(player => player.score).ThenBy(player => player.name);

    public void Finish(int id, float score, string name)
    {
        if (!playerFinishList.Any(item => item.id == id)) InitializePlayer(id, name, score);

    }
}

