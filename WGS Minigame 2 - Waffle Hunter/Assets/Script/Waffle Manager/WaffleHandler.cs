using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleHandler : MonoBehaviour
{
    public float waffle;
    ShieldHandler handler;
    Animator _anim;

    [Header("UI Component")]
    [SerializeField] Text waffleTextUI;

    private void Awake()
    {
        handler = GetComponent<ShieldHandler>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
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

        if (other.tag == "Waffle")
        {
            IncreaseWaffle();
        }
    }

    public void IncreaseWaffle()
    {
        waffle++;
        waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
    }

    public void DecreaseWaffle()
    {
        waffle--;
        _anim.SetTrigger("Hit");
        //GetComponent<PlayerController>().enabled = false;
    }
}
