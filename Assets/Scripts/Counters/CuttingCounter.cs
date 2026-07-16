using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO cutKitchenObjectSO;
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())// When there is an Object on the counter we cut it
        {
            // There is an Object on the counter

            // Destroy previous GameObject/KitchenObect
            GetKitchenObject().DestroySelf();

            // Instanciate the sliced variant
            /*Transform kitchenObjectTransform = Instantiate(cutKitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);*/
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchenObject on the counter
            if( player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this); // This is a good example of why its good to write clean code... Its so easy to scale now!
            } 
            else
            {
                // Player is not carrying anything nothing

            }
        } 
        else
        {
            // There is a kitchenObject on the counter
            if (player.HasKitchenObject())
            {
                // player is carrying something

            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
