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

    public bool itemIsActive;

    // Start is called before the first frame update
    void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void Start()
    {
        _light.range = defaultLightRange;
        itemIsActive = false;
    }

    private void Update()
    {
        if (itemIsActive)
        {
            StartCoroutine(ActivateItem());
        }
    }

    IEnumerator ActivateItem()
    {
        IncreaseVisibility();

        yield return new WaitForSeconds(itemTime);

        backToDefault();
    }

    public void IncreaseVisibility()
    {
        _light.range = newLightRange;
        transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);
    }

    public void backToDefault()
    {
        _light.range = defaultLightRange;
        transform.position = new Vector3(transform.position.x, defaultPositionY, transform.position.z);
        itemIsActive = false;
    }
}
