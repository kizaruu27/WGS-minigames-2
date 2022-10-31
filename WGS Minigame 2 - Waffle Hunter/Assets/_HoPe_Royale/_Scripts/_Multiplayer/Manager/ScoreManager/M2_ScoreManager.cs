using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class M2_ScoreManager : MonoBehaviour
{
    [Header("Players List")]
    public List<M2_MPlayerScore> ScorePlayerList;

    public void SetPlayersData(int _playerId, string _playerName, float _playerScore)
    {
        M2_MPlayerScore PScore = new M2_MPlayerScore();

        PScore.PActorNumber = _playerId;
        PScore.PlayerName = _playerName;
        PScore.PlayerScore = _playerScore;

        ScorePlayerList.Add(PScore);
    }

    public void AddPlayerData(int _playerId, string _playerName, float _playerScore)
    {
        if (!ScorePlayerList.Any(item => item.PActorNumber == _playerId)) SetPlayersData(_playerId, _playerName, _playerScore);
    }

    public void SetPlayerScore(int _playerid, float _playerScore)
    {
        var playerScore = ScorePlayerList.Find((x) => x.PActorNumber == _playerid);
        playerScore.PlayerScore = _playerScore;
    }

    public IEnumerable<M2_MPlayerScore> GetPlayerDataScore() => ScorePlayerList.OrderByDescending(player => player.PlayerScore).ThenBy(player => player.PlayerName);
}