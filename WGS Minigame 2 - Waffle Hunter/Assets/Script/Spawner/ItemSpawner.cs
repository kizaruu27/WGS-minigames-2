using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Items;
    [SerializeField] float spawnTime;

    PhotonView view;

    private void Awake() => view = GetComponent<PhotonView>();

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
        view.RPC("SpawnToAll", RpcTarget.AllBuffered, spawnIndex);

        yield return new WaitForSeconds(spawnTime);

        StartCoroutine(SpawnItems());
    }

    [PunRPC]
    void SpawnToAll(int spawnIndex) =>
        Instantiate(Items[spawnIndex].gameObject, transform.position, Quaternion.identity);
}
