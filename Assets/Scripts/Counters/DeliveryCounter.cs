using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // Only accepts plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                plateKitchenObject.DestroySelf();
            }
            // When I dont use else if but just if it crashes... i dont know why
            else if (player.GetKitchenObject().TryGetDynamicPlate(out DynamicPlateKitchenObject dynamicPlateKitchenObject))
            {
                // Only accepts plates
                dynamicPlateKitchenObject.DestroySelf();
            }
        }
    }
}
