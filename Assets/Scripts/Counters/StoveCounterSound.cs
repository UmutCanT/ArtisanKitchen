using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
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
}
