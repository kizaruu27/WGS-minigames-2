using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{

    public GameObject shield;
    public float shieldTime;
    public bool shieldActivated;

    private void Start() 
    {
        shieldActivated = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Shield")
        {
            StartCoroutine(ActivateShield());
            shieldActivated = true;
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
        shieldActivated = false;
    }
}
