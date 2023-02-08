using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Animator characterAnimator;
    
    // Update is called once per frame
    void Update()
    {
        characterAnimator.SetBool(IS_WALKING, characterMovement.IsWalking);
    }
}
