using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static event EventHandler<OnVolumeChangeEventArgs> OnVolumeChange;

    public class OnVolumeChangeEventArgs : EventArgs
    {
        public float volume;
    }

    [SerializeField] private AudioSource audioSource;

    private float volume = .5f;

    private void Start()
    {
        audioSource.volume = volume;

        OnVolumeChange?.Invoke(this, new OnVolumeChangeEventArgs
        {
            volume = volume
        });
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1.01f)
            volume = 0f;

        audioSource.volume = volume;
        OnVolumeChange?.Invoke(this, new OnVolumeChangeEventArgs
        {
            volume = volume
        });
    }
}
