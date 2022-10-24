using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_ListOnPlayer : MonoBehaviour
{
    public static List<M2_ListOnPlayer> playerList = new List<M2_ListOnPlayer>();

    public static List<M2_ListOnPlayer> getPlayerList()
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
