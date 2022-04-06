using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    //[SerializeField] private Transform target;
    public string targetTag;


    // Update is called once per frame
    void Update()
    {
        var target = GameObject.FindWithTag(targetTag);


        Vector3 targetPosition = target.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
    }
}
