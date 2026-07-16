using UnityEngine;

public class CuttingCounter : BaseCounter
{
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
