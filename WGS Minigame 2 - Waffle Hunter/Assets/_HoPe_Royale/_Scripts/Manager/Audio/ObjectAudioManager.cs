using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAudioManager : MonoBehaviour
{
    public static ObjectAudioManager instance;

    public AudioSource audioSource;
    public AudioClip speedItemAudioClip;
    public AudioClip directionItemAudioClip;
    public AudioClip shieldItemAudioClip;
    public AudioClip waffleAudioClip;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySpeedAudio()
    {
        audioSource.PlayOneShot(speedItemAudioClip);
    }

    public void PlayDirectionAudio()
    {
        audioSource.PlayOneShot(directionItemAudioClip);
    }

    public void PlayShieldAudio()
    {
        audioSource.PlayOneShot(shieldItemAudioClip);
    }

    public void PlayWaffleAudio()
    {
        audioSource.PlayOneShot(waffleAudioClip);
    }
}
