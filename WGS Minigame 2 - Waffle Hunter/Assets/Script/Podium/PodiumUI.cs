using UnityEngine;
using Photon.Pun;
using System.Linq;
using System.Collections.Generic;

public class PodiumUI : MonoBehaviour
{
    public static PodiumUI instance;

    [Header("Components")]
    public PodiumManager finishManager;
    public PodiumStandingItem row;

    [Header("Player List")]

    IEnumerable<MPodium> PlayerFinish;
    List<PodiumStandingItem> cachePlayerList = new List<PodiumStandingItem>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        RemovePlayerListCache();
        ShowPlayerList();


        WaitingPlayerToFinish();
        PlayerDiscHighlight();

        if (cachePlayerList.Count == (int)PhotonNetwork.CurrentRoom.MaxPlayers) return;



    }

    public void ShowPlayerList()
    {
        PlayerFinish = finishManager.GetLeaderboardData();

        foreach (var item in PlayerFinish.Select((value, index) => new { index, value }))
        {
            var rowData = Instantiate(row, transform);

            rowData.SetColorItem(item.value.id == PhotonNetwork.LocalPlayer.ActorNumber - 1 || item.value.id == 0);

            rowData.Rank.text = GenerateRankText(item.index);
            rowData.Name.text = item.value.name.Length <= 20 ? item.value.name : item.value.name.Substring(0, 20) + "...";
            rowData.Score.text = item.value.score.ToString();

            cachePlayerList.Add(rowData);
        }
    }

    public void WaitingPlayerToFinish()
    {
        if (PhotonNetwork.CurrentRoom.MaxPlayers > cachePlayerList.Count)
        {
            for (int i = 0; i < ((int)PhotonNetwork.CurrentRoom.MaxPlayers - finishManager.TotalPlayersDisconnect) - cachePlayerList.Count; i++)
            {
                var rowData = Instantiate(row, transform);

                rowData.Rank.text = "";
                rowData.Name.text = "Waiting Other Player";
                rowData.Score.text = "";

                cachePlayerList.Add(rowData);
            }
        }
    }

    public void PlayerDiscHighlight()
    {
        int PDC = finishManager.TotalPlayersDisconnect;
        if (PDC > 0)
        {
            for (int i = 0; i < PDC; i++)
            {
                var rowData = Instantiate(row, transform).GetComponent<PodiumStandingItem>();

                rowData.SetHighlightPlayerDC();

                rowData.Rank.text = "";
                rowData.Name.text = "Disconnected";
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

    public void OnClickBackToMenu()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(2);
    }
}
