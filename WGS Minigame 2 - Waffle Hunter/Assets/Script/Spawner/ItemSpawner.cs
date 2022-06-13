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

        if (view.IsMine)
        {
            view.RPC("SpawnItems", RpcTarget.AllBuffered);
        }

    }

    [PunRPC]
    IEnumerator SpawnItems()
    {
        int spawnIndex = Random.Range(0, Items.Length);
        // view.RPC("SpawnToAll", RpcTarget.AllBuffered, spawnIndex);
        Instantiate(Items[spawnIndex].gameObject, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnTime);

        StartCoroutine(SpawnItems());
    }
}
