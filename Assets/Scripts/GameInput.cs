using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interct_performed;
        playerInputActions.Player.InteractAlternate.performed += InterctAlternate_performed;
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
