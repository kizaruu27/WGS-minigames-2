using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownSpeed : MonoBehaviour
{
    public float slowDown;
    public float defaultSpeed;

    public float itemTime;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(SlowDownPlayer(col));
        }
    }

    IEnumerator SlowDownPlayer(Collider col)
    {
        col.GetComponent<PlayerController>().playerSpeed = slowDown;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(itemTime);

        col.GetComponent<PlayerController>().playerSpeed = defaultSpeed;
        Destroy(gameObject);
    }
}
