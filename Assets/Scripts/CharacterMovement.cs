using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private const float PATH_CHECK_RADIUS = .7f;
    private const float PATH_CHECK_HEIGHT = 2f;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    
    private bool isWalking;
    private IMovementInput movementInput;
  
    public bool IsWalking { get => isWalking; }

    private void Awake()
    {
        movementInput = GetComponent<IMovementInput>();
    }

    private void Update()
    {
        Movement();
    }

    private bool CanMove(Vector3 moveDir, float moveDistance)
    {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PATH_CHECK_HEIGHT, PATH_CHECK_RADIUS, moveDir, moveDistance);
    }

    private void Movement()
    {
        Vector2 inputVector = movementInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;

        if (!CanMove(moveDir, moveDistance))
        {
            //Can't move towards to target location
            //Attempt only X direction
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            if (CanMove(moveDirX, moveDistance))
            {
                moveDir = moveDirX;
            }
            else
            {
                //Can't move on X axis
                //Attempt only Z direction
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

                if (CanMove(moveDirZ, moveDistance))
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (CanMove(moveDir, moveDistance))
        {
            transform.position += moveDistance * moveDir;
        }

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
}
