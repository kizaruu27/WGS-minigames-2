using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private PhotonView pv;
    
    public static PlayerAudioManager instance;

    public AudioSource audioSource;
    public AudioClip footstepClip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public void PlayFootstepAudio()
    {
        audioSource.PlayOneShot(footstepClip);
    }
}
