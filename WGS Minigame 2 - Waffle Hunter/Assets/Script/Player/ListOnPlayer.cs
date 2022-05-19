using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOnPlayer : MonoBehaviour
{
    public static List<ListOnPlayer> playerList = new List<ListOnPlayer>();

    public static List<ListOnPlayer> getPlayerList()
    {
        return playerList;
    }

    private void Awake()
    {
        playerList.Add(this);
    }

    public void outRange()
    {
        playerList.Remove(this);
    }
}
