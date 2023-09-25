using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : Singleton<AudioPlayer>
{
    [SerializeField] private AudioSource _source;

    public void PlaySound(AudioClip sound, float volume = 1f)
    {
        _source.PlayOneShot(sound, volume);
    }
}
