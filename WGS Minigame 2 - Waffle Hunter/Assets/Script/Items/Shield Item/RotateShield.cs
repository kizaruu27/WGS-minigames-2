using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShield : MonoBehaviour
{
    public float rotationSpeed;
    public Transform player;
    
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 1.2f, player.position.z);
        transform.Rotate(Vector3.up * rotationSpeed);
    }
}
