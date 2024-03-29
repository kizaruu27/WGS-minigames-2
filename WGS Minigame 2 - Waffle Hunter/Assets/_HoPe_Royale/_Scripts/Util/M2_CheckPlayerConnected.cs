using UnityEngine;
using System.Collections;
using Photon.Pun;
using System;


public class M2_CheckPlayerConnected : MonoBehaviour
{
    public void ActivateController()
    {
        var controller = FindObjectsOfType<M2_PlayerControllerV2>();

        foreach (var controllers in controller)
        {
            controllers.canMove = true;
        }
    }

    public IEnumerator WaitAllPlayerReady(Action ActionMethod)
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Player").Length == (int)PhotonNetwork.PlayerList.Length);

        ActionMethod();
    }
}