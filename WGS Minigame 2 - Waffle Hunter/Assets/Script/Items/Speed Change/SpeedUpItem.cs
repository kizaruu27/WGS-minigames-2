using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    public string namePowerUp;
    public float speedUp;
    public float defaultSpeed;

    public float itemTime;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(SpeedUpPlayer(col));
        }
    }

    IEnumerator SpeedUpPlayer(Collider col)
    {
        col.GetComponent<PlayerControllerV2>().playerSpeed = speedUp;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(itemTime);

        col.GetComponent<PlayerControllerV2>().playerSpeed = defaultSpeed;
        Destroy(gameObject);
    }
}
