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

    PlayerController playerController;
    // public SpeedChanger speedChanger;

    // public string namePowerUp;


    // Start is called before the first frame update
    private void Awake()
    {
        playerController = GetComponentInChildren<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlusSpeed()
    {
        SpeedChanger Changer = FindObjectOfType<SpeedChanger>();

        if (Changer.namePowerUp == "SpeedUp")
        {
            Debug.Log("Speed Increase");
            StartCoroutine(SpeedBoostCooldown());
        } 

        else if (Changer.namePowerUp == "SlowDown")
        {
            Debug.Log("Speed Decrease");
            StartCoroutine(SlowBoostCooldown());
        }
        // playerController.playerSpeed *= 1.5f;

    }

    public void BackToNormalSpeed()
    {
        playerController.playerSpeed = normalSpeed;
    }

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
