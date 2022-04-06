using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseVisibilityItem : MonoBehaviour
{
    private Light _light;
    public float newLightRange;
    public float defaultLightRange;
    public float itemTime;
    public float newPositionY;
    public float defaultPositionY;

    // Start is called before the first frame update
    void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Start()
    {
        _light.range = defaultLightRange;
    }

    public void IncreaseVisibility()
    {
        _light.range = newLightRange;
        transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);
        Invoke("backToDefault", itemTime);
    }

    void backToDefault()
    {
        _light.range = defaultLightRange;
        transform.position = new Vector3(transform.position.x, defaultPositionY, transform.position.z);
    }
}
