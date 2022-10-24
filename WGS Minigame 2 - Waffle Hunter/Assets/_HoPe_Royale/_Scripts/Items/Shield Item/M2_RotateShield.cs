using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_RotateShield : MonoBehaviour
{
    public float rotationSpeed;
    public Transform player;

    void Update()
    {
        // transform.position = new Vector3(player.position.x, player.position.y + 1.2f, player.position.z);

        if (this.gameObject.activeSelf == true)
            transform.Rotate(Vector3.up * rotationSpeed);
    }
}
