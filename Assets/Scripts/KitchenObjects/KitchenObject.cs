using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // This entire script is just so that the prefabs know which scriptable Object they are categorized in
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    //private ClearCounter clearCounter; -> this is specific for counters we want it generally to all kitchen object parents
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectsSO GetKitchenObjectsSO()
    {
        return kitchenObjectsSO;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        // Destroy(this); Doesnt appear to work, at least it doesnt delete the visual
        Destroy(gameObject); // Ok so destroy this litarly only destroys this script/component while gameObject well destroys the entire gameobject with everything connected
    }

    public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
    {
        // If there is something on the current counter clear it
        if (kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject(); // Tell the "previous" counter to clear its kitchenobject
        }
        // Set the new counter of the kitchenobject
        kitchenObjectParent = newKitchenObjectParent;
        // safety the should never be two objects on the counter
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("KitchenObjectParent already has kitchenObject");
        }
        // tell the new kitchencounter that it has a "new" kitchenobject
        kitchenObjectParent.SetKitchenObject(this);
        // Litarly just swap the parent for a new one, new counterTopPoint!
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero; // For safety
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    // The static is the reason we can do this anywhere
    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject; // cast
            return true;
        } else
        {
            plateKitchenObject = null;
            return false;
        }
    }
    public bool TryGetDynamicPlate(out DynamicPlateKitchenObject dynamicPlateKitchenObject)
    {
        if (this is DynamicPlateKitchenObject)
        {
            dynamicPlateKitchenObject = this as DynamicPlateKitchenObject; // cast
            return true;
        } else
        {
            dynamicPlateKitchenObject = null;
            return false;
        }
    }
}