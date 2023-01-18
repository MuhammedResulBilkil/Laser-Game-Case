using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    public AudioSource audioSource;
    public AudioClip[] clips;
    public bool isSoundOff;

  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();



    }


    public void PlayAudioClip(AudioClip audio)
    {

        AudioClip clip = audioSource.GetComponent<AudioClip>();
        clip = audio;
        if (!isSoundOff) audioSource.PlayOneShot(clip);
      

    }
}