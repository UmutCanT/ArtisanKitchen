using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    private const float INTERACT_DISTANCE = 2f;

    [SerializeField] private LayerMask countersLayerMask;

    private PlayerInput playerInput;
    private ClearCounter selectedCounter;
    private Vector3 lastInteractDir;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.OnInteractAction += PlayerInput_OnInteractAction;
    }

    private void PlayerInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, INTERACT_DISTANCE, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    ChangeSelectedCounter(clearCounter);                   
                }
            }
            else
            {
                ChangeSelectedCounter(null);
            }
        }
        else
        {
            ChangeSelectedCounter(null);
        }
    }

    private void ChangeSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }
}