using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static event EventHandler OnStep;

    private const float FOOT_STEP_INTERVAL_MAX = .1f;

    [SerializeField] private CharacterMovement movement;
    private float footStepInterval;


    private void Update()
    {
        if (movement.IsWalking)
        {
            footStepInterval -= Time.deltaTime;
            if (footStepInterval < 0f)
            {
                footStepInterval = FOOT_STEP_INTERVAL_MAX;
                OnStep?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
