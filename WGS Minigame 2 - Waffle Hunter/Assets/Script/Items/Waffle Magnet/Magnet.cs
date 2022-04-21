using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public static bool isMagnet;

    // Start is called before the first frame update
    void Start()
    {
        isMagnet = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isMagnet = true;
            Destroy(gameObject);
        }
    }
}
