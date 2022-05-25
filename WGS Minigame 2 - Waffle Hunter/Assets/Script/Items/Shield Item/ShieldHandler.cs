using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    public GameObject shield;
    public float shieldTime;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Shield")
        {
            StartCoroutine(ActivateShield());
        }
    }

    IEnumerator ActivateShield()
    {
        shield.SetActive(true);

        yield return new WaitForSeconds(shieldTime);

        DeactivateShield();
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }
}
