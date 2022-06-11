using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleHandler : MonoBehaviour
{
    public float waffle; //ini waffle
    ShieldHandler handler;

    [Header("UI Component")]
    [SerializeField] Text waffleTextUI;
    [SerializeField] GameObject MenuUI;

    private void Awake()
    {
        handler = GetComponent<ShieldHandler>();
    }

    private void Start()
    {
        //waffle = 0;
        //waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
    }


    private void Update() 
    {
        if (waffle >= 10)
        {
            Time.timeScale = 0;
            //MenuUI.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Waffle")
        {
            IncreaseWaffle();
        }
    }

    void IncreaseWaffle()
    {
        waffle++;
        //waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
    }

    public void DecreaseWaffle()
    {
        waffle--;
        if (waffle <= 0)
        {
            waffle = 0;
        }

       //waffleTextUI.text = "Waffle Collected: " + waffle.ToString();
    }

    void ActivateController()
    {
        GetComponent<PlayerController>().enabled = true;
    }
}
