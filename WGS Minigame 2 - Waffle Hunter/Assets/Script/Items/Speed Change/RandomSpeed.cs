using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public class objectSpawn
{
    [Range(1, 100)] public int rate;
    public GameObject prefab;
}

public class RandomSpeed : MonoBehaviour
{


    public List<objectSpawn> objects;
    protected List<GameObject> items;

    void Start()
    {
        items = new List<GameObject>();
    }

    void AddItem(GameObject gameObject)
    {
        items.Add(gameObject);
    }

    public void SpawnSpeedRandom()
    {
        GameObject go = getItem();

        if (go != null)
        {
            AddItem(Instantiate(go, transform.position, Quaternion.identity));
        }
    }

    private GameObject getItem()
    {
        int limit = 0;

        foreach (objectSpawn osr in objects)
        {
            limit += osr.rate;
        }

        int random = Random.Range(0, limit);

        foreach (objectSpawn osr in objects)
        {
            if (random < osr.rate)
            {
                return osr.prefab;
            }
            else
            {
                random -= osr.rate;
            }
        }

        return null;
    }
}
