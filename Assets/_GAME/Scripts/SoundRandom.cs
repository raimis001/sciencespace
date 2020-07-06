using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundRandom : MonoBehaviour
{
  public AudioClip[] clips;

  AudioSource _source;
  AudioSource source => _source ? _source : _source = GetComponent<AudioSource>();
  
  public void Play(bool force = true,  int sound = -1)
	{
    if (sound < 0)
      sound = Random.Range(0, clips.Length);

    if (force && source.isPlaying)
      source.Stop();

    source.clip = clips[sound];
    source.Play();
	}
}
