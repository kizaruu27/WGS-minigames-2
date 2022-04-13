using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PlayerController player = col.GetComponent<PlayerController>();
            player.playerSpeed = 3f;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<PlayerController>().playerSpeed = 5;
        }
    }


}
