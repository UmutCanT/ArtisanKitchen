using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterAnimation : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter; 
    [SerializeField] private GameObject stoveVisual;
    [SerializeField] private GameObject fryingParticles;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        stoveVisual.SetActive(IsActive(e.state));
        fryingParticles.SetActive(IsActive(e.state));
    }

    private bool IsActive(StoveCounter.State state) => state == StoveCounter.State.Fried || state == StoveCounter.State.Frying;
}
