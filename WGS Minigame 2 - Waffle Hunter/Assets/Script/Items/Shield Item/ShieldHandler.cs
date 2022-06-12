using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShieldHandler : MonoBehaviour
{
    public GameObject shield;
    public float shieldTime;
    public bool shieldActivated;

    private void Start() 
    {
        shieldActivated = false;
    }

    private void Update() 
    {
        if (shieldActivated)
        {
            StartCoroutine(ActivateShield());
        }   
        else
        {
            DeactivateShield();
        } 
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Shield")
        {
            shieldActivated = true;
        }
    }

    IEnumerator ActivateShield()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            shield.SetActive(true);

            yield return new WaitForSeconds(shieldTime);

            shieldActivated = false;
        }

    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }
}
