using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobject : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] GameObject particle;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
