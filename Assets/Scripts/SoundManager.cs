using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClipReferences audioClipReferences;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void PlaySoundEffect(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySoundEffect(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    
    private void PlaySoundEffect(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
