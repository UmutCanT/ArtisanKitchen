using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject objectCanProgress;
    [SerializeField] private Image barImage;

    private ICanProgress canProgress;

    private void Start()
    {
        canProgress = objectCanProgress.GetComponent<ICanProgress>();
        canProgress.OnProgressChanged += ObjectCanProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        ShowProgressBar(false);
    }

    private void ObjectCanProgress_OnProgressChanged(object sender, ICanProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            ShowProgressBar(false);
        }
        else
        {
            ShowProgressBar(true);
        }
    }

    private void ShowProgressBar(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
