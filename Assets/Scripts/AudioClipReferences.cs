using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipReferences", menuName = "AudioClipRefs")]
public class AudioClipReferences : ScriptableObject
{
    [SerializeField] private AudioClip[] chop;
    [SerializeField] private AudioClip[] deliverySuccess;
    [SerializeField] private AudioClip[] deliveryFailed;
    [SerializeField] private AudioClip[] footStep;
    [SerializeField] private AudioClip[] objectDrop;
    [SerializeField] private AudioClip[] objectPickUp;
    [SerializeField] private AudioClip[] trash;
    [SerializeField] private AudioClip[] warning;
    [SerializeField] private AudioClip stoveSizzle;

    public AudioClip[] Chop => chop;
    public AudioClip[] DeliverySuccess => deliverySuccess;
    public AudioClip[] DeliveryFailed => deliveryFailed;
    public AudioClip[] FootStep => footStep;
    public AudioClip[] ObjectDrop => objectDrop;
    public AudioClip[] ObjectPickUp => objectPickUp;
    public AudioClip[] Trash => trash;
    public AudioClip[] Warning => warning;
    public AudioClip StoveSizzle => stoveSizzle;
}
