using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{

    private float speed;
    private float normalSpeed = 5f;
    private float speedBoost = 8.5f;
    private float speedBoostDuration = 5f;
    private float slowSpeed = 3f;

    M2_PlayerController _m2PlayerController;
    // public SpeedChanger speedChanger;

    // public string namePowerUp;


    // Start is called before the first frame update
    private void Awake()
    {
        _m2PlayerController = GetComponentInChildren<M2_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedChange")
        {
            M2_SpeedUpItem Changer = other.GetComponent<M2_SpeedUpItem>();

            if (Changer.namePowerUp == "SpeedUp")
            {

                StartCoroutine(SpeedBoostCooldown());
            }

            if (Changer.namePowerUp == "SlowDown")
            {
                StartCoroutine(SlowBoostCooldown());
            }
            // playerController.playerSpeed *= 1.5f;
        }
    }

    // public void PlusSpeed()
    // {
    //     SpeedChanger Changer = GetComponent<SpeedChanger>();

    //     if (Changer.namePowerUp == "SpeedUp")
    //     {

    //         StartCoroutine(SpeedBoostCooldown());
    //     }

    //     if (Changer.namePowerUp == "SlowDown")
    //     {
    //         StartCoroutine(SlowBoostCooldown());
    //     }
    //     // playerController.playerSpeed *= 1.5f;

    // }

    IEnumerator SpeedBoostCooldown()
    {
        _m2PlayerController.playerSpeed = speedBoost;
        yield return new WaitForSeconds(speedBoostDuration);
        _m2PlayerController.playerSpeed = normalSpeed;

    }

    IEnumerator SlowBoostCooldown()
    {
        _m2PlayerController.playerSpeed = slowSpeed;
        yield return new WaitForSeconds(speedBoostDuration);
        _m2PlayerController.playerSpeed = normalSpeed;
    }

}
