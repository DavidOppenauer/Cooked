using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event EventHandler OnSelectedCounterChanged;
    // for extending the base capability of the eventsystem:
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float interactDistance = 2f;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInterAction;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInput_OnInterAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if( moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if(Physics.Raycast(transform.position,  lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            // If the following statement is true
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))// Tryget automatically handles null references
            {
                // Has Clear Counter
                if ( clearCounter != selectedCounter)
                {
                    selectedCounter = clearCounter;
                }
            } // if it hits something but its not a clearCounter
            else
            {
                selectedCounter = null;
            }
            
        }// if the raycast doesnt hit anything there is no counter
        else
        {
            selectedCounter = null;
        }
        Debug.Log(selectedCounter);
    }

    public bool GetIsWalking()
    {
        return isWalking;
    }


    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        // Set isWalking
        isWalking = moveDir != Vector3.zero;

        // moveSpeed = moveDistance/time
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if(!canMove)
        {
            // Cannot move towards moveDir

            // Attempt only x Movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            // We try to fire the cast only in the direction of the x component of the moveDir Vector
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                // Cannot Move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                // Fire only in the z component of the MovedirVector positive and negative is handled by WASD input.
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMove)
                {
                    // Can Move only on Z
                    moveDir = moveDirZ;
                } 
                else
                {
                    // We cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}