using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    // Only this and classes that extend this can use the protected stuff
    // protected 
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    // For every function that you want the chuild classes to implement in their own way you can use virtual
    public virtual void Interact(Player player) // abstract is also fine
    {
        Debug.LogError("BaseCounter.Interact();");
    }
    public virtual void InteractAlternate(Player player) // abstract is also fine
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    // All the being a kitchenObject parent related stuff
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
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