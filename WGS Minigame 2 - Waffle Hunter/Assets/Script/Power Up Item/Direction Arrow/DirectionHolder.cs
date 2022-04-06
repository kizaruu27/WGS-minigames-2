using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionHolder : MonoBehaviour
{
    public GameObject Direction;
    // Start is called before the first frame update


    public void ShowDirection()
    {
        Direction.SetActive(true);
    }

    public void hidDirection()
    {
        Direction.SetActive(false);
    }
}
