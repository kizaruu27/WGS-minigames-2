using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_WaffleBehaviour : MonoBehaviour
{
    public bool waffleCollected;
    public GameObject particleEffect;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            ValidateWaffle();
            StartCoroutine(DestroyWaffle());
        }
    }

    public void ValidateWaffle()
    {
        waffleCollected = true;
    }

    IEnumerator DestroyWaffle()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        Instantiate(particleEffect, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(.5f);

        Destroy(gameObject);
    }
}
