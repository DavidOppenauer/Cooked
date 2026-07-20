using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnOvenStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnOvenStateChangedEventArgs : EventArgs
    {
        public State _state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burnt
    }

    private State state;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    [SerializeField] private Player player;
    // Coroutines for time based stuff
    /*private void Start()
    {
        StartCoroutine(HandleFryTimer());
    }
    private IEnumerator HandleFryTimer()
    {
        yield return new WaitForSeconds(1f);
    }*/
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                
                    break;
                case State.Frying:
                        fryingTimer += Time.deltaTime;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                        });
                        if(fryingTimer > fryingRecipeSO.fryingTimerMax)
                        {
                            // Fried
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this); // Huh I could just spawn it on the player... Idea for another time

                            state = State.Fried;
                            burningTimer = 0f;
                            // After it switches to the cooked one we can get the new Recipe, cause its not the uncooked one anymore
                            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());

                            OnStateChanged?.Invoke(this, new OnOvenStateChangedEventArgs {_state = state} );
                        }
                    break;
                case State.Fried:
                        burningTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                        });

                        if(burningTimer > burningRecipeSO.burningTimerMax)
                        {
                            // Fried
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this); // Huh I could just spawn it on the player... Idea for another time

                            state = State.Burnt;
                            OnStateChanged?.Invoke(this, new OnOvenStateChangedEventArgs {_state = state} );

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                                progressNormalized = 0f
                            });
                        }
                    break;
                case State.Burnt:
                
                    break;

            }
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
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO()))
                {                
                    // Player has something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this); // This is a good example of why its good to write clean code... Its so easy to scale now!

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectsSO());

                    // Will this work or will it switch before reseting timer...
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnOvenStateChangedEventArgs {_state = state} );
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
            } 
            else
            {
                // Player is not carrying anything

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

                state = State.Idle;
                
                OnStateChanged?.Invoke(this, new OnOvenStateChangedEventArgs {_state = state} );
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                    progressNormalized = 0f
                });
            }
        }
    }
    private KitchenObjectsSO GetOutPutForInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        return null;
    }
    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        
        if (fryingRecipeSO != null)
        {
            return true;
        }
        
        return false;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
