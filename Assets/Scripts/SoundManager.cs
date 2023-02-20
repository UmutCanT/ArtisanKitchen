using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static event EventHandler<OnVolumeChangeEventArgs> OnVolumeChange;

    public class OnVolumeChangeEventArgs : EventArgs 
    { 
        public float volume; 
    }

    private const float DEFAULT_VOLUME = .4f;

    [SerializeField] AudioClipReferences audioClipReferences;

    private float volume = 1f;

    private void Start()
    {
        volume = PlayerPreferences.LoadSfxVolume();

        OnVolumeChange?.Invoke(this, new OnVolumeChangeEventArgs
        {
            volume = volume
        });
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.OnObjectPickUp += Player_OnObjectPickUp;
        BaseCounter.OnAnyObjectDropped += BaseCounter_OnAnyObjectDropped;
        TrashCounter.OnAnyObjectThrashed += TrashCounter_OnAnyObjectThrashed;
        PlayerSounds.OnStep += PlayerSounds_OnStep;
    }

    private void OnDestroy()
    {
        CuttingCounter.OnAnyCut -= CuttingCounter_OnAnyCut;
        Player.OnObjectPickUp -= Player_OnObjectPickUp;
        BaseCounter.OnAnyObjectDropped -= BaseCounter_OnAnyObjectDropped;
        TrashCounter.OnAnyObjectThrashed -= TrashCounter_OnAnyObjectThrashed;
        PlayerSounds.OnStep -= PlayerSounds_OnStep;
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1.01f)
            volume = 0f;

        PlayerPreferences.SaveSfxVolume(volume);

        OnVolumeChange?.Invoke(this, new OnVolumeChangeEventArgs 
        { 
            volume = volume 
        });
    }

    private void PlayerSounds_OnStep(object sender, System.EventArgs e)
    {
        PlayerSounds footStep = sender as PlayerSounds;
        PlaySoundEffect(audioClipReferences.FootStep, footStep.transform.position);
    }

    private void TrashCounter_OnAnyObjectThrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySoundEffect(audioClipReferences.Trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectDropped(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySoundEffect(audioClipReferences.ObjectDrop, baseCounter.transform.position);
    }

    private void Player_OnObjectPickUp(object sender, System.EventArgs e)
    {
        Player player = sender as Player;
        PlaySoundEffect(audioClipReferences.ObjectPickUp, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySoundEffect(audioClipReferences.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = sender as DeliveryManager;
        PlaySoundEffect(audioClipReferences.DeliveryFailed, deliveryManager.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryManager deliveryManager = sender as DeliveryManager;
        PlaySoundEffect(audioClipReferences.DeliverySuccess, deliveryManager.transform.position);
    }

    private void PlaySoundEffect(AudioClip[] audioClipArray, Vector3 position, float volume = DEFAULT_VOLUME)
    {
        PlaySoundEffect(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    
    private void PlaySoundEffect(AudioClip audioClip, Vector3 position, float volumeMultiplier = DEFAULT_VOLUME)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
}
