using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaffleManager : MonoBehaviour
{
    public GameObject waffle;
    public Transform[] spawnPosition;


    // Update is called once per frame
    void Update()
    {
        AddWaffleScript addWaffle;
        addWaffle = FindObjectOfType<AddWaffleScript>();
        
        if (addWaffle.waffleCollected)
        {
            StartCoroutine(spawnWaffle());
        }
    }

    IEnumerator spawnWaffle()
    {
        Instantiate(waffle, spawnPosition[Random.Range(0, spawnPosition.Length)].position, Quaternion.identity);

        yield return new WaitForSeconds(1);

        FindObjectOfType<AddWaffleScript>().waffleCollected = false;
    }
}
