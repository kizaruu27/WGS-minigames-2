using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waffle : MonoBehaviour
{

    public DirectionHolder directionHolder;

    // Start is called before the first frame update
    void Start()
    {

        directionHolder = GetComponent<DirectionHolder>();
    }

    // Update is called once per frame
    void Update()
    { }

    public void CheckShowing()
    {

        if (directionHolder.isShowing == true)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            // gameObject.GetComponent<BoxCollider>().enabled = true;
        }

    }
}
