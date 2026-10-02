using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interct_performed;
        playerInputActions.Player.InteractAlternate.performed += InterctAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        //Use the Unsubscribe in tandem
        playerInputActions.Player.Interact.performed -= Interct_performed;
        playerInputActions.Player.InteractAlternate.performed -= InterctAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        // With the Dispose, just to be safe
        playerInputActions.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interct_performed(UnityEngine.InputSystem.InputAction.CallbackContext ob)
    {
        // First part till the ? operator finds out if there even are listeners or rather if there are none its null
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    private void InterctAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext ob)
    {
        // First part till the ? operator finds out if there even are listeners or rather if there are none its null
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        return inputVector;
    }

}
