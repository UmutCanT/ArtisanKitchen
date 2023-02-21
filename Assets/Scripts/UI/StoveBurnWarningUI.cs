using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        UIActive(false);
    }

    private void StoveCounter_OnProgressChanged(object sender, ICanProgress.OnProgressChangedEventArgs e)
    {
        UIActive(stoveCounter.IsFried() && stoveCounter.WarningProgressAmount < e.progressNormalized);
    }

    public void UIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
