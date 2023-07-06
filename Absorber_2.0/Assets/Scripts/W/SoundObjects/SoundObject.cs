using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    AudioSource audioSource;

    AudioClip clip;


    public void Play()
    {
        audioSource.PlayOneShot(audioSource.clip);
        Destroy(gameObject, audioSource.clip.length);
    }


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop =false;
        audioSource.playOnAwake = false;
    }
}
