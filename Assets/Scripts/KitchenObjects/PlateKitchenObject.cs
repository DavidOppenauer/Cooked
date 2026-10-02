using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject // The plate is just a more specific type of kitchenObject
{ // For inheritance you should have a very good reason to do it... here its fine cause it extends this just a little bit

    public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;
    public class OnIngridientAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectsSO> validKitchenObjectSOList;
    private List<KitchenObjectsSO> kitchenObjectsSOList;

    private void Awake()
    {
        kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }
    // I can modify this to make more recipes/ tomatonator with a *two whole tomatos in the buns for example
    public void AddIngridient(KitchenObjectsSO kitchenObjectsSO)
    {
        kitchenObjectsSOList.Add(kitchenObjectsSO);
    }
    public bool TryAddIngridient(KitchenObjectsSO _kitchenObjectSO) // Check if there is a duplicate of the parameter kitchenSO
    {
        if (!validKitchenObjectSOList.Contains(_kitchenObjectSO))
        {
            // Not a valid ingridient
            return false;
        }
        /*if (kitchenObjectsSOList.Contains(_kitchenObjectSO))
        {
            // Already has that ingridient
            return false;
        }*/ else
        {
            kitchenObjectsSOList.Add(_kitchenObjectSO);

            OnIngridientAdded?.Invoke(this,  new OnIngridientAddedEventArgs{kitchenObjectSO = _kitchenObjectSO});

            return true;
        }
    }

    public List<KitchenObjectsSO> GetKitchenObjectSOList()
    {
        return kitchenObjectsSOList;
    }
}
