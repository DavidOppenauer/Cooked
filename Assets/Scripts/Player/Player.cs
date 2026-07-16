using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent // With the beatiful Interface it is absolutly possible to have enemys steal your items!
{
    // Singleton Pattern, there is only ever gonna be one player so we can make it static
    /*private static Player instance;
    public static Player Instance
    {
        get { return instance; }
        set { instance = value; }
    }*/
    // Same as this
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    // for extending a C# event to pass in some more data
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounterArgs;
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
    private BaseCounter selectedCounter;

    // Interface specific stuff
    private KitchenObject kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private void Awake()
    {
        // Set the Singleton so everyone can grab something
        if ( Instance != null)
        {
            Debug.LogError("There should only be one player, there is one too many");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInterAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInterActionAlternate;
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
            selectedCounter.Interact(this); // Is inside the new baseclass
        }
    }
    private void GameInput_OnInterActionAlternate(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this); // Is inside the new baseclass
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
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))// Tryget automatically handles null references
            {
                // Has Clear Counter
                if ( baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            } // if it hits something but its not a clearCounter
            else
            {
                SetSelectedCounter(null);
            }
            
        }// if the raycast doesnt hit anything there is no counter
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter _selectedCounter)
    {
        this.selectedCounter = _selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounterArgs = _selectedCounter
        });
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
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

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
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

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
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    // HERE YOU COULD PLAY A LITTLE ANIMATION
    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        kitchenObject = _kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}