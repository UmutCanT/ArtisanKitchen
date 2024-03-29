﻿using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IMovementInput
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    
    private PlayerInputActions playerInputActions;

    public enum Binding
    {
        Move_Up,
        Move_Down, 
        Move_Left, 
        Move_Right,
        Interact,
        InteractAlternate,
        Pause
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        if (PlayerPreferences.InputBindingsHasRecord())
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPreferences.LoadInputBindings());
        }
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Interact.performed += PressAButtonToStartTheGame;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;        
    }

    private void PressAButtonToStartTheGame(InputAction.CallbackContext obj)
    {
        GameManager.Instance.InteractActionToStartCountdown();
        playerInputActions.Player.Interact.performed -= PressAButtonToStartTheGame;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    public string GetBindingText(Binding binding)
    {
        return binding switch
        {
            Binding.Move_Up => playerInputActions.Player.Move.bindings[1].ToDisplayString(),
            Binding.Move_Down => playerInputActions.Player.Move.bindings[2].ToDisplayString(),
            Binding.Move_Left => playerInputActions.Player.Move.bindings[3].ToDisplayString(),
            Binding.Move_Right => playerInputActions.Player.Move.bindings[4].ToDisplayString(),
            Binding.Interact => playerInputActions.Player.Interact.bindings[0].ToDisplayString(),
            Binding.InteractAlternate => playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString(),
            Binding.Pause => playerInputActions.Player.Pause.bindings[0].ToDisplayString(),
            _ => throw new NotImplementedException(),
        };
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();
            PlayerPreferences.SaveInputBindings(playerInputActions.SaveBindingOverridesAsJson());
        }).Start();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        GameManager.Instance.TogglePauseGame();
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }   
}
