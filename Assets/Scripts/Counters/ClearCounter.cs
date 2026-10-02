using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())// Player is carraying something and theres nothing on the counter place it down
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
        else // there is nothing on the player and he isnt carrying something give it to the player
        {
            // There is a kitchenObject on the counter
            if (player.HasKitchenObject())
            {
                // player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is holding a Plate
                    // I like writing the comments like this it helps you to get on track faster
                    if( plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectsSO())) // We need to cast it so we can use the .AddIngridient function
                    {
                        GetKitchenObject().DestroySelf();
                    } 
                } 
                else if (player.GetKitchenObject().TryGetDynamicPlate(out DynamicPlateKitchenObject dynamicPlateKitchenObject))
                {
                    // Player is holding a dynamic Plate
                    if(dynamicPlateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectsSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is not holding a plate
                    if(GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectOnPlayer)) // This is in the context now below or at least inside the condition thats what out is for
                    {
                        // Counter is holding a plate
                        if(plateKitchenObjectOnPlayer.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectsSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                    // Player is not holding a dynamicPlate
                    if(GetKitchenObject().TryGetDynamicPlate(out DynamicPlateKitchenObject dynamicPlateKitchenObjectOnPlayer)) // This is in the context now below or at least inside the condition thats what out is for
                    {
                        // Counter is holding a dynamic plate
                        if(dynamicPlateKitchenObjectOnPlayer.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectsSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


    /*[SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;*/

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

    /*public override void Interact(Player player) // Overrides the base function
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
    */
}