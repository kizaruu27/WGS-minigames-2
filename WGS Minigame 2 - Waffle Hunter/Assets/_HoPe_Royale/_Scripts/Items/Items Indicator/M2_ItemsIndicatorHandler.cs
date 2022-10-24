using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class M2_ItemsIndicatorHandler : MonoBehaviour
{
    [Header("Indicator Component")]
    [SerializeField] GameObject indicator;

    [Header("Timer Component")]
    [SerializeField] Text timer;
    [SerializeField] float itemTime;
    [SerializeField] float currentTime;
    [SerializeField] bool isActive;

    PhotonView pv;

    private void Awake() {
        pv = GetComponent<PhotonView>();
    }


    private void Start()
    {
        itemTime = currentTime;
    }

  
    private void Update()
    {
        if (isActive)
        {
            CalculateTimer();
        }
        if (itemTime <= 0)
        {
            StopCalculating();
            deactivateIndicator();
        }
       
    }

    public void activateIndicator()
    {
        isActive = true;
        indicator.SetActive(true);

    }

    public void deactivateIndicator()
    {
        indicator.SetActive(false);
        StopCalculating();
    }

    void CalculateTimer()
    {
        itemTime -= Time.deltaTime;
        float seconds = Mathf.FloorToInt(itemTime % 60);
        timer.text = seconds.ToString();   
    }

    void StopCalculating()
    {
        itemTime = currentTime;
        timer.text = null;
        isActive = false;
    }
}
