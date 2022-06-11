using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownObstacle : MonoBehaviour
{
    [SerializeField] float defaultSpeed = 5f;
    [SerializeField] float slowSpeed = 3f;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PlayerControllerV2 player = col.GetComponent<PlayerControllerV2>();
            player.playerSpeed = slowSpeed;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerControllerV2>().playerSpeed = defaultSpeed;
        }
    }


}
