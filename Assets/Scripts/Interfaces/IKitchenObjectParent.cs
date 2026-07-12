using UnityEngine;

public interface IKitchenObjectParent // With the beatiful Interface it is absolutly possible to have enemys steal your items!
{
    // All the being a kitchenObject parent related stuff

    public Transform GetKitchenObjectFollowTransform();
    
    public void SetKitchenObject(KitchenObject _kitchenObject);
    
    public KitchenObject GetKitchenObject();
    
    public void ClearKitchenObject();
   
    public bool HasKitchenObject();
}