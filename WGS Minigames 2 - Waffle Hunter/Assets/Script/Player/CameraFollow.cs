using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject TargetPlayer;
    private Vector3 offset;
    private Vector3 newtrans;
    
    void Start()
    {
        offset.x = transform.position.x - TargetPlayer.transform.position.x;
        offset.z = transform.position.z - TargetPlayer.transform.position.z;
        newtrans = transform.position;
    }

    void LateUpdate()
    {
        newtrans.x = TargetPlayer.transform.position.x + offset.x;
        newtrans.z = TargetPlayer.transform.position.z + offset.z;
        transform.position = newtrans;
    }
}
