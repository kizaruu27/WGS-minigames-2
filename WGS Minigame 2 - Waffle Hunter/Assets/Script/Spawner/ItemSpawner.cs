using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Items;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Items[Random.Range(0, Items.Length)], transform.position, Quaternion.identity);
    }
}
