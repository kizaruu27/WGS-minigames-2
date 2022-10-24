using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_SlowDownObstacle : MonoBehaviour
{
    [SerializeField] float defaultSpeed = 5f;
    [SerializeField] float slowSpeed = 3f;
    [SerializeField] float slowSpeedNPC = 2f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            M2_PlayerControllerV2 m2Player = col.GetComponent<M2_PlayerControllerV2>();
            m2Player.playerSpeed = slowSpeed;
        }

        if (col.tag == "NPC")
        {
            var NPC = col.GetComponent<M2_NpcController>().agent;
            NPC.speed = slowSpeedNPC;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<M2_PlayerControllerV2>().playerSpeed = defaultSpeed;
        }

        if (col.tag == "NPC")
        {
            var NPC = col.GetComponent<M2_NpcController>().agent;
            NPC.speed = defaultSpeed;
        }
    }


}
