using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVisibilityItem : MonoBehaviour
{
    public float visibilityValue;
    public float defaultVisibilityValue;
    public float itemTime;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(IncreaseVisibility(col));
        }
    }

    IEnumerator IncreaseVisibility(Collider col)
    {
        col.GetComponentInChildren<Light>().spotAngle = visibilityValue;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(itemTime);

        col.GetComponentInChildren<Light>().spotAngle = defaultVisibilityValue;
        Destroy(gameObject);

    }
}
