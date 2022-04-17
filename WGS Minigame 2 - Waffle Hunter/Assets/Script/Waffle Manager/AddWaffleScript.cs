using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWaffleScript : MonoBehaviour
{
    public ScriptableValue waffleValue;

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
        Destroy(gameObject);
    }
}
