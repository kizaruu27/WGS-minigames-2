using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class M2_SpawnWaffleManager : MonoBehaviour
{
    public GameObject waffle;
    public Transform[] spawnPosition;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            int spawnPoint = Random.Range(0, spawnPosition.Length);
            // Instantiate(waffle, spawnPosition[spawnPoint].position, Quaternion.identity);
            view.RPC("RPC_InstantiateWaffle", RpcTarget.AllBuffered, spawnPoint);
        }
    }


    // Update is called once per frame
    void Update()
    {
        M2_WaffleBehaviour m2Waffle;
        m2Waffle = FindObjectOfType<M2_WaffleBehaviour>();

        // Debug.Log(waffle.waffleCollected);

        if (m2Waffle.waffleCollected == true && m2Waffle != null)
        {
            int randomIndexSpawn = Random.Range(0, spawnPosition.Length);
            int currentIndex = randomIndexSpawn + 1;

            if (currentIndex > spawnPosition.Length)
            {
                currentIndex = 0;
            }

            if (view.IsMine)
            {
                // StartCoroutine(spawnWaffle(currentIndex));
                view.RPC("RPC_InstantiateWaffle", RpcTarget.AllBuffered, randomIndexSpawn);
            }
        }
    }

    IEnumerator spawnWaffle(int index)
    {
        view.RPC("RPC_InstantiateWaffle", RpcTarget.OthersBuffered, index);

        yield return new WaitForSeconds(.5f);
        FindObjectOfType<M2_WaffleBehaviour>().waffleCollected = false;
    }

    [PunRPC]
    IEnumerator RPC_InstantiateWaffle(int index)
    {
        // if (!view.IsMine) return;

        Instantiate(waffle, spawnPosition[index].position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<M2_WaffleBehaviour>().waffleCollected = false;
    }


}
