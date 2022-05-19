using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOnPlayer : MonoBehaviour
{
    public static List<ListOnPlayer> playerList = new List<ListOnPlayer>();
    public Transform trialPlayer;

    public static List<ListOnPlayer> getPlayerList()
    {
        return playerList;
    }

    private void Awake()
    {
        playerList.Add(this);
        trialPlayer = GameObject.FindWithTag("Player").transform;
    }

    public void TagPlayer()
    {

    }
    public void outRange()
    {
        playerList.Remove(this);
    }
}
