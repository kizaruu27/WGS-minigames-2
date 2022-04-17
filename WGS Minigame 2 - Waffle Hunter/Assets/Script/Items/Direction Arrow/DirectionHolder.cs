using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionHolder : MonoBehaviour
{
    public GameObject Direction;
    public Transform directionSpawnPoint;
    public float itemTime;
    public bool isShowing;


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Direction")
        {
            ShowDirection();
            Destroy(col.gameObject);
        }
    }

    public void ShowDirection()
    {
        isShowing = true;
        Direction.SetActive(true);
        Invoke("hidDirection", itemTime);
    }

    public void hidDirection()
    {
        Direction.SetActive(false);
        isShowing = false;
    }
}
