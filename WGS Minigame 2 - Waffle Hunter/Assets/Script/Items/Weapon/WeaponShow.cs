using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShow : MonoBehaviour
{
    public GameObject Sword;
    bool isShowing;

    private void Start()
    {
        Sword.SetActive(false);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Sword")
        {
            ShowDirection();
            Destroy(col.gameObject);
        }
    }

    public void ShowDirection()
    {
        isShowing = true;
        Sword.SetActive(true);

    }
}
