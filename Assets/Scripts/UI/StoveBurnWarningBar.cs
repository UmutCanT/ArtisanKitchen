using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningBar : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private Animator animator;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, ICanProgress.OnProgressChangedEventArgs e)
    {
        animator.SetBool(IS_FLASHING, stoveCounter.IsFried() && stoveCounter.WarningProgressAmount < e.progressNormalized);
    }
}
