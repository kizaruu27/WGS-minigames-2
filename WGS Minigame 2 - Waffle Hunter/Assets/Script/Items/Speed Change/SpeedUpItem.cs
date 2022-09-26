using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    public string namePowerUp;
    public float speedUp;
    public float defaultSpeed;

    public float itemTime;

    [Header("Item Mesh")] 
    [SerializeField] private GameObject itemMesh;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(SpeedUpPlayer(col));
        }
    }

    IEnumerator SpeedUpPlayer(Collider col)
    {
        itemMesh.SetActive(false);
        ObjectAudioManager.instance.PlaySpeedAudio();
        col.GetComponent<PlayerControllerV2>().playerSpeed = speedUp;
        GetComponent<CapsuleCollider>().enabled = false;
        

        yield return new WaitForSeconds(itemTime);

        col.GetComponent<PlayerControllerV2>().playerSpeed = defaultSpeed;
        Destroy(gameObject);
    }
}
