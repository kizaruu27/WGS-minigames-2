using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{

    private float speed;
    // private float normalSpeed = 5f;
    private float speedBoost = 8.5f;
    private float speedBoostDuration = 5f;

    PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlusSpeed()
    {
        StartCoroutine(SpeedBoostCooldown());
    }

    IEnumerator SpeedBoostCooldown()
    {
        speed = speedBoost;
        yield return new WaitForSeconds(speedBoostDuration);
        speed = playerController.playerSpeed;
    }

}
