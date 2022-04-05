using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleHunterSpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject Item;
    public Transform[] ItemSpawnPoints;
    public Transform[] playerSpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        int itemSpawnIndex = Random.Range(0, ItemSpawnPoints.Length);
        int playerSpawnIndex = Random.Range(0, playerSpawnPoints.Length);

        if (itemSpawnIndex == playerSpawnIndex)
        {
            if (itemSpawnIndex > ItemSpawnPoints.Length - 2)
            {
                itemSpawnIndex = 0;
            }
            else
            {
                itemSpawnIndex += 2;
            }
            
            
        }
        
        Instantiate(Item, ItemSpawnPoints[itemSpawnIndex].position, transform.rotation);
        Instantiate(Player, playerSpawnPoints[playerSpawnIndex].position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
