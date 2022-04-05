using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TriggerEvent : MonoBehaviour
{
    [Header("Unity Event")]
    public UnityEvent onTriggerEvent;
    
    [Header("Target Tag")]
    public string targetTag;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == targetTag)
        {
            onTriggerEvent?.Invoke();
        }
    }
}
