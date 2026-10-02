using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    /// <summary>
    /// FINALYYYYYYYYYYYYYYYYYY THE EVENT PROBLEM IS ADRESSED WITH THE STATIC EVENT
    /// Its an event that belongs to the entire class, so you dont need reference to every single instance
    /// BUT
    /// You need to reset them, its a whole thing 9:04:00
    /// </summary>
    
    // Here it comes video 7:53:16
    public static event EventHandler OnAnyCut; // When any instance cuts, fire this

    new public static void ResetStaticData() // Clears all the listeners on the cutting counter
    {
        OnAnyCut = null;
    }
    // Here it ends
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchenObject on the counter
            if( player.HasKitchenObject())
            {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO()))
                {                
                    // Extra check if the player is holding something that has a valid recipe and only then be able to drop it
                    player.GetKitchenObject().SetKitchenObjectParent(this); // This is a good example of why its good to write clean code... Its so easy to scale now!
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax // In an int equation at least one needs to be a float else result will be int and possibly 0 instead 0f 0.2
                    }); // sender was this object normally we just EventArgs.Empty but with the custom one we can send data
                }
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
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(this.GetKitchenObject().GetKitchenObjectsSO()))// When there is an Object on the counter and it has a valid recipe we cut it
        {
            // There is an Object on the counter AND it can be cut
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            Debug.Log(OnAnyCut.GetInvocationList().Length);
            OnAnyCut?.Invoke(this, EventArgs.Empty); // When any instance cuts, fire this
            KitchenObjectsSO currentKitchenObjectSO = GetKitchenObject().GetKitchenObjectsSO();
            // Find out what to spawn
            // KitchenObjectsSO outPutKitchenObjectSO = GetOutPutForInput(player.GetKitchenObject()); <- maybe a thought for something else. bitch like what????
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(currentKitchenObjectSO);

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax // In an int equation at least one needs to be a float else result will be int and possibly 0 instead 0f 0.2
            }); // sender was this object normally we just EventArgs.Empty but with the custom one we can send data

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) // if it we press enough then spawn it
            {            
                KitchenObjectsSO outputKitchenObjectSO = GetOutPutForInput(currentKitchenObjectSO);

                // Destroy previous GameObject/KitchenObect
                GetKitchenObject().DestroySelf();

                // Instanciate the sliced variant
                /*Transform kitchenObjectTransform = Instantiate(cutKitchenObjectSO.prefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);*/
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }


        }
    }
    private KitchenObjectsSO GetOutPutForInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }
    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        
        if (cuttingRecipeSO != null)
        {
            return true;
        }
        
        return false;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}