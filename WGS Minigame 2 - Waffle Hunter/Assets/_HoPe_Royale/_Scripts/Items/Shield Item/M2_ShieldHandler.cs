using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class M2_ShieldHandler : MonoBehaviour
{
    public GameObject shield;
    public float shieldTime;
    public bool shieldActivated;
    public PhotonView pv;

    private void Start()
    {
        // shieldActivated = false;
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            if (shieldActivated == true)
            {
                pv.RPC("SetActive", RpcTarget.AllViaServer, true);
            }
            else
            {
                pv.RPC("SetActive", RpcTarget.AllViaServer, false);
            }
        }

        StartCoroutine(ActivateShield(shieldActivated));
    }

    private void OnTriggerEnter(Collider col)
    {
        if (pv.IsMine)
        {
            if (col.tag == "Shield")
            {
                shieldActivated = true;
                M2_ObjectAudioManager.instance.PlayShieldAudio();
            }
        }
    }

    IEnumerator ActivateShield(bool act)
    {
        if (act == true)
        {
            shield.SetActive(true);

            yield return new WaitForSeconds(shieldTime);

            shieldActivated = false;
        }
        else
        {
            shield.SetActive(false);
        }
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

    [PunRPC]
    void SetActive(bool act)
    {
        shieldActivated = act;
    }
}
