using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaffleManager : MonoBehaviour
{
    public ScriptableValue waffleCollected;
    public GameObject waffle;
    public Transform[] spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (waffleCollected.validation)
        // {
        //     StartCoroutine(spawnWaffle());
        // }
    }

    IEnumerator spawnWaffle()
    {
        Instantiate(waffle, spawnPosition[Random.Range(0, spawnPosition.Length)].position, Quaternion.identity);

        yield return new WaitForSeconds(1);

        waffleCollected.validation = false;
    }
}
