using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleHandler : MonoBehaviour
{
    public float waffle;
    ShieldHandler handler;
    Animator _anim;

    private void Awake()
    {
        handler = GetComponent<ShieldHandler>();
        _anim = GetComponentInChildren<Animator>();
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
        _anim.SetTrigger("Hit");
        //GetComponent<PlayerController>().enabled = false;
    }
}
