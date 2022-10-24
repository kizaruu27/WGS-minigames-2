using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_Waffle : MonoBehaviour
{

    public M2_DirectionHolder m2DirectionHolder;

    // Start is called before the first frame update
    void Start()
    {

        m2DirectionHolder = GetComponent<M2_DirectionHolder>();
    }

    // Update is called once per frame
    void Update()
    { }

    public void CheckShowing()
    {

        if (m2DirectionHolder.isShowing == true)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            // gameObject.GetComponent<BoxCollider>().enabled = true;
        }

    }
}
