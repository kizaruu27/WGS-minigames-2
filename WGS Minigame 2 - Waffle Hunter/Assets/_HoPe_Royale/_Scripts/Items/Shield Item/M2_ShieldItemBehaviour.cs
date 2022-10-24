using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_ShieldItemBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(DestroyItem(col));
        }
    }

    IEnumerator DestroyItem(Collider col)
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(col.GetComponent<M2_ShieldHandler>().shieldTime);

        Destroy(gameObject);
        
    }
}
