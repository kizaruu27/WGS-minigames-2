using UnityEngine;
using Photon.Pun;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class M2_PodiumUI : MonoBehaviourPunCallbacks
{
    public static M2_PodiumUI instance;

    [Header("Components")]
    public M2_ScoreManager m2ScoreManager;
    public M2_PodiumStandingItem row;

    [Header("Player List")]

    IEnumerable<M2_MPlayerScore> PlayerFinish;
    List<M2_PodiumStandingItem> cachePlayerList = new List<M2_PodiumStandingItem>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        RemovePlayerListCache();
        ShowPlayerList();

        if (!M2_GameFlowManager.instance.isDone)
        {
            WaitingPlayerToFinish();

            if (cachePlayerList.Count == (int)PhotonNetwork.CurrentRoom.PlayerCount) return;
        }
    }

    public void ShowPlayerList()
    {
        PlayerFinish = m2ScoreManager.GetPlayerDataScore();

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
        SceneManager.LoadScene("WGS2_Lobby");
    }

    public override void OnLeftRoom()
    {
        Debug.LogError("Left room");
        PhotonNetwork.LoadLevel("WGS2_Lobby");
    }
}
