using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICanCarryKitchenObject
{
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public static event EventHandler OnObjectPickUp;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    private const float INTERACT_DISTANCE = 2f;

    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform objectHoldingPoint;

    private PlayerInput playerInput;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private Vector3 lastInteractDir;

    public KitchenObject KitchenObj { get => kitchenObject; 
        set 
        { 
            kitchenObject = value;
            if (kitchenObject != null)
            {
                OnObjectPickUp?.Invoke(this, EventArgs.Empty);
            }
        }}

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.OnInteractAction += PlayerInput_OnInteractAction;
        playerInput.OnInteractAlternateAction += PlayerInput_OnInteractAlternateAction;
    }

    private void PlayerInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void PlayerInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    ChangeSelectedCounter(baseCounter);                   
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

    private void ChangeSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

    public Transform GetParentTransform() => objectHoldingPoint;

    public void ClearKitchenObject() => kitchenObject = null;

    public bool HasKitchenObject() => kitchenObject != null;
}