using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2_UIAnimationHandler : MonoBehaviour
{
    public Animator anim;

    public void PlayNotifAnimation()
    {
        anim.SetTrigger("Play");
    }
}
