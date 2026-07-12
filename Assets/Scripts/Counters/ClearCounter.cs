using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    //Testing
    /*[SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;
    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }*/

    public void Interact(Player player)
    {
        // If co kitchenObject, spawn it, if there aleady is one then do nothing
        if(kitchenObject == null)
        {
            // Debug.Log("Interact!");
            // Access the prefab REFERENCE of the scriptable Object
            Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            
            // All in one litarly just more compact...
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

            // get reference to the new kitchenObjects(cheese tomato whatever) script component
            //kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            // This line shows individuality i guess not just prefab but actual instance yeah
            //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectsSO().objectName);

            // Assign this counter to that new kitchenObject
            //kitchenObject.SetClearCounter(this); 
        } 
        else
        {
            // Give the object to the player
            // We have to use an Interface to have both the player and the counter be able to be a parent of the kitchenobject
            kitchenObject.SetKitchenObjectParent(player);
        }
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