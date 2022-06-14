using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationHandler : MonoBehaviour
{
    public Animator anim;

    public void PlayNotifAnimation()
    {
        anim.Play("FadeAnimation");
    }
}
