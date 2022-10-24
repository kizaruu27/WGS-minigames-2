using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_RotateObject : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
