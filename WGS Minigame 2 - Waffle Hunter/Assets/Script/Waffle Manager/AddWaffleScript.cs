using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWaffleScript : MonoBehaviour
{
    public ScriptableValue waffleValue;
    public bool waffleCollected;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            AddWaffle();
        }
    }

    public void AddWaffle()
    {
        waffleValue.value++;
        Destroy(gameObject, 0.01f);
        waffleCollected = true;
    }
}
