using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionHolder : MonoBehaviour
{
    public GameObject Direction;
    public float itemTime;


    public void ShowDirection()
    {

        Direction.SetActive(true);
        Invoke("hidDirection", itemTime);

    }

    public void hidDirection()
    {
        Direction.SetActive(false);
    }
}
