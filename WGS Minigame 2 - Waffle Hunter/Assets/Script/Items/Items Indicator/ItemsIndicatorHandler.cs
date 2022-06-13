using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsIndicatorHandler : MonoBehaviour
{
    [SerializeField] GameObject indicator;

    public void activateIndicator()
    {
        indicator.SetActive(true);
    }

    public void deactivateIndicator()
    {
        indicator.SetActive(false);
    }
}
