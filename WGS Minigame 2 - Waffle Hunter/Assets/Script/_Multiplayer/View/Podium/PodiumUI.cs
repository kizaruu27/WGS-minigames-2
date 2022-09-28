using UnityEngine;
using Photon.Pun;
using System.Linq;
using System.Collections.Generic;

public class PodiumUI : MonoBehaviourPunCallbacks
{
    public static PodiumUI instance;

    [Header("Components")]
    public ScoreManager scoreManager;
    public PodiumStandingItem row;

    [Header("Player List")]

    IEnumerable<MPlayerScore> PlayerFinish;
    List<PodiumStandingItem> cachePlayerList = new List<PodiumStandingItem>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        RemovePlayerListCache();
        ShowPlayerList();

        if (!GameFlowManager.instance.isDone)
        {
            WaitingPlayerToFinish();

            if (cachePlayerList.Count == (int)PhotonNetwork.CurrentRoom.PlayerCount) return;
        }
    }

    public void ShowPlayerList()
    {
        PlayerFinish = scoreManager.GetPlayerDataScore();

        foreach (var item in PlayerFinish.Select((value, index) => new { index, value }))
        {
            var rowData = Instantiate(row, transform);

            rowData.SetColorItem(item.value.PActorNumber == PhotonNetwork.LocalPlayer.ActorNumber);

            rowData.Rank.text = GenerateRankText(item.index);
            rowData.Name.text = item.value.PlayerName.Length <= 20 ? item.value.PlayerName : item.value.PlayerName.Substring(0, 20) + "...";
            rowData.Score.text = item.value.PlayerScore.ToString();

            cachePlayerList.Add(rowData);
        }
    }

    public void WaitingPlayerToFinish()
    {
        if (PhotonNetwork.CurrentRoom.MaxPlayers > cachePlayerList.Count)
        {
            for (int i = 0; i < (int)PhotonNetwork.CurrentRoom.PlayerCount - cachePlayerList.Count; i++)
            {
                var rowData = Instantiate(row, transform);

                rowData.Rank.text = "";
                rowData.Name.text = "Waiting Other Player";
                rowData.Score.text = "";

                cachePlayerList.Add(rowData);
            }
        }
    }


    public void RemovePlayerListCache()
    {
        foreach (var item in cachePlayerList) Destroy(item.gameObject);

        cachePlayerList.Clear();
    }

    string GenerateRankText(int rank)
    {
        int indexPosition = rank + 1;

        return indexPosition switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            _ => $"{indexPosition}th"
        };
    }

    public void OnClickExitRoom()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.LogError("Left room");
        PhotonNetwork.LoadLevel("WGS2_Lobby");
    }
}
