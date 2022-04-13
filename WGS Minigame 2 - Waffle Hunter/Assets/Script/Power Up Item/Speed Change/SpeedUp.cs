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

    public PlayerController playerController;
    // public SpeedChanger speedChanger;

    // public string namePowerUp;


    // Start is called before the first frame update
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedChange")
        {
            SpeedChanger Changer = other.GetComponent<SpeedChanger>();

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
        playerController.playerSpeed = speedBoost;
        yield return new WaitForSeconds(speedBoostDuration);
        playerController.playerSpeed = normalSpeed;

    }

    IEnumerator SlowBoostCooldown()
    {
        playerController.playerSpeed = slowSpeed;
        yield return new WaitForSeconds(speedBoostDuration);
        playerController.playerSpeed = normalSpeed;
    }

}
