using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    private IMovementInput movementInput;

    private bool isWalking;
    public bool IsWalking { get => isWalking; }

    private void Awake()
    {
        movementInput = GetComponent<IMovementInput>();
    }

    private void Update()
    {
        Vector2 inputVector = movementInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.position += moveSpeed * Time.deltaTime * moveDir;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
}
