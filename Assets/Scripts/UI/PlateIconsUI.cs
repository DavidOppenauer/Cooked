using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
    }

    private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform) // Cycles through everything on the object
        {
            if (child == iconTemplate)
            {
                continue; // It skips this specific child, we need it further down
            }
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectsSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            // you can grab the transform of what you instanciate to customize it right after instanciation
           Transform iconTransform = Instantiate(iconTemplate, this.transform); // if you pass in transform as the parent it knows its this object
           iconTransform.gameObject.SetActive(true);
           iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
