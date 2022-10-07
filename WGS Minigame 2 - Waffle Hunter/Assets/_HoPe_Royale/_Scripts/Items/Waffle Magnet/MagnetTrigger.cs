using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrigger : MonoBehaviour
{
    private Transform Player;
    public float magnetDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Magnet.isMagnet == true)
        {
            if(Vector3.Distance(transform.position, Player.position) < magnetDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, Player.position, 0.1f);
            }
            //transform.position = Vector3.MoveTowards(transform.position, Player.position, 0.1f);
        }
    }
}
