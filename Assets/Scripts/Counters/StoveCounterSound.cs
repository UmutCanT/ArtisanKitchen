using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    public static event EventHandler OnWarning;

    private const float WARNING_SOUND_MAX_INTERVAL = .2f;

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private AudioSource audioSource;

    bool isWarning = false;
    bool isPlaying = false;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, ICanProgress.OnProgressChangedEventArgs e)
    {
        isWarning = stoveCounter.IsFried() && stoveCounter.WarningProgressAmount < e.progressNormalized;
        if (isWarning && !isPlaying)
        {
            StartCoroutine(PlayWarningSound());
        }        
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        if (IsActive(e.state))
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }      
    }

    private bool IsActive(StoveCounter.State state) => state == StoveCounter.State.Fried || state == StoveCounter.State.Frying;

    IEnumerator PlayWarningSound()
    {
        isPlaying= true;
        while(isWarning)
        {              
            yield return new WaitForSeconds(WARNING_SOUND_MAX_INTERVAL);
            OnWarning?.Invoke(this, EventArgs.Empty);
        }
        isPlaying= false;
    }
}
