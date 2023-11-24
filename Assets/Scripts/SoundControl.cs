using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundControl : MonoBehaviour
{
    public AudioClip wahwah;
    public AudioClip loopmusic;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        StartCoroutine(playSound());
    }
    
    IEnumerator playSound()
    {
        _audioSource.clip = wahwah;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length + 5);
        _audioSource.clip = loopmusic;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
