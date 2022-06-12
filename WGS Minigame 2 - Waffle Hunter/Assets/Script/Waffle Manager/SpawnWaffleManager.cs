using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaffleManager : MonoBehaviour
{
    public GameObject waffle;
    public Transform[] spawnPosition;


    private void Start()
    {
        Instantiate(waffle, spawnPosition[Random.Range(0, spawnPosition.Length)].position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        WaffleBehaviour waffle;
        waffle = FindObjectOfType<WaffleBehaviour>();
        
        if (waffle.waffleCollected)
        {
            int randomIndexSpawn = Random.Range(0, spawnPosition.Length);
            int currentIndex = randomIndexSpawn + 1;

            if (currentIndex > spawnPosition.Length)
            {
                currentIndex = 0;
            }


            StartCoroutine(spawnWaffle(currentIndex));
        }
    }

    IEnumerator spawnWaffle(int index)
    {
        Instantiate(waffle, spawnPosition[index].position, Quaternion.identity);

        yield return new WaitForSeconds(.5f);

        FindObjectOfType<WaffleBehaviour>().waffleCollected = false;
    }
}
