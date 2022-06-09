using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobject : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] GameObject particle;

    private void Start()
    {
        Invoke("DestroyObject", destroyTime);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
