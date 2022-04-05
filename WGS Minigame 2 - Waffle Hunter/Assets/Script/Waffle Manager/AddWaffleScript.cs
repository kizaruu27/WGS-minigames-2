using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWaffleScript : MonoBehaviour
{
    public ScriptableValue waffleValue;

    public void AddWaffle()
    {
        waffleValue.value++;
    }
}
