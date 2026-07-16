using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    /*[SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;*/
    public override void Interact(Player player)
    {
        // If no kitchenObject, spawn it, if there aleady is one then do nothing
        if(!HasKitchenObject())
        {
            // If player doesnt have a KitchenObject, Give kitchenObject to player
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectsSO, player);
                // Debug.Log("Interact!");
                // Access the prefab REFERENCE of the scriptable Object
                //Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
                //Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
                //kitchenObjectTransform.localPosition = Vector3.zero;

                // All in one litarly just more compact...
                // For this counter we want to give it to the player immeadiatly, not spawn it on Top
                // kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

                //kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                // Player animation here for example
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }


            // get reference to the new kitchenObjects(cheese tomato whatever) script component
            //kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            // This line shows individuality i guess not just prefab but actual instance yeah
            //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectsSO().objectName);

            // Assign this counter to that new kitchenObject
            //kitchenObject.SetClearCounter(this); 
        } 
        /*else
        {
            // Give the object to the player if there is already an object on the counter
            // We have to use an Interface to have both the player and the counter be able to be a parent of the kitchenobject
            // kitchenObject.SetKitchenObjectParent(player);
        }*/
    }

}
