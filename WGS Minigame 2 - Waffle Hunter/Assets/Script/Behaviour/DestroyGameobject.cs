using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobject : MonoBehaviour
{
    [SerializeField] float destroyTime;

    private void Start()
    {
        Invoke("DestroyObject", destroyTime);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
