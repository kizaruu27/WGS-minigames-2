using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Items;
    [SerializeField] float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnItems());
        //DestroyItem();

        StartCoroutine(SpawnItems());
    }


    IEnumerator SpawnItems()
    {
        int spawnIndex = Random.Range(0, Items.Length);

        Instantiate(Items[spawnIndex].gameObject, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnTime);

        StartCoroutine(SpawnItems());
    }
}
