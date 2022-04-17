using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVisibilityItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.GetComponentInChildren<IncreaseVisibilityItem>().itemIsActive = true;
            Destroy(gameObject);
        }
    }
}
