using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    // A list of recipe lists
    //private List<List<KitchenObject>> recipeList;
    // The same thing but with an extra name property

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer = 0f;
    [SerializeField] private float spawnRecipeTimerMax = 4f;
    [SerializeField] private int waitingRecipesMax = 5;

    private int successfulRecipesAmount;

    private void Awake()
    {

        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            // This spawns the recipes
            if(GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
            {
                // Spawn a new "random" recipe from the list
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                //Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject _plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            // Early check if the plate contents length matches the recipe/order length, if its different it doesnt get checked
            if (waitingRecipeSO.kitchenObjectSOList.Count == _plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has the same number of ingridients

                bool plateContentsMatchRecipe = true;
                foreach (KitchenObjectsSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    // Cycling through all ingridiedients in the RECIPE
                    bool ingriedientFound = false;
                    foreach (KitchenObjectsSO plateKitchenObjectSO in _plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycling through all contents/ingridients of the PLATE
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // this ingriedient is matching
                            ingriedientFound = true;
                            break; // I dont know if this doesnt backfire
                        }
                    }
                    if (!ingriedientFound)
                    {
                        // This recipe ingridient was not found on the Plate
                        // Not a single matching thing was found?
                        plateContentsMatchRecipe = false;
                    }
                }

                if (plateContentsMatchRecipe)
                {
                    // Player delivered the correct Recipe!
                    //Debug.Log("Player delivered the correct recipe!");
                    successfulRecipesAmount++;

                    waitingRecipeSOList.RemoveAt(i); // Remove the completed recipe from the waitinglist

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    return; // If we find a waitingrecipe that matches we are done
                }
            }
        }
        // No matches found
        // player did not deliver a correct recipe
        Debug.Log("player did not deliver a correct recipe");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}