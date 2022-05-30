using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleHandler : MonoBehaviour
{
    public float waffle;
    ShieldHandler handler;

    private void Awake()
    {
        handler = GetComponent<ShieldHandler>();
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (handler.shieldActivated != true)
            {
                DecreaseWaffle();
                Debug.Log("Kena Player");
            }
        }
    }

    public void IncreaseWaffle()
    {
        waffle++;
    }

    public void DecreaseWaffle()
    {
        waffle--;
    }
}
