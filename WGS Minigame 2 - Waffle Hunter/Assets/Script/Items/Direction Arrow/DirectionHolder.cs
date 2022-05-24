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
            ShowDirection(col);
            Destroy(col.gameObject, itemTime);
        }
    }

    public void ShowDirection(Collider col)
    {
        isShowing = true;
        Direction.SetActive(true);
        Invoke("hidDirection", itemTime);
        col.GetComponent<MeshRenderer>().enabled = false;
        col.GetComponent<BoxCollider>().enabled = false;
    }

    public void hidDirection()
    {
        Direction.SetActive(false);
        isShowing = false;
    }
}
