using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionHolder : MonoBehaviour
{
    public GameObject Direction;
    public float itemTime;
    public bool isShowing;


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
