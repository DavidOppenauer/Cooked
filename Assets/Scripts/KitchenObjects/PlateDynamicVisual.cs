using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateDynamicVisual : MonoBehaviour
{
    [SerializeField] private Transform plateTopPoint;
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private DynamicPlateKitchenObject dynamicPlateKitchenObject;
    //[SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private List<GameObject> dynamicPlateVisualGameObjectList;

    private void Awake()
    {
        dynamicPlateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        dynamicPlateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
    }

    private void PlateKitchenObject_OnIngridientAdded(object sender, DynamicPlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        Transform ingridientVisualTransform = Instantiate(e.kitchenObjectSO.prefab, plateTopPoint);
        float ingridientOffsetY = 0.5f;
        ingridientVisualTransform.localPosition = new Vector3(0, ingridientOffsetY * dynamicPlateVisualGameObjectList.Count, 0);

        dynamicPlateVisualGameObjectList.Add(ingridientVisualTransform.gameObject);
        //e.kitchenObjectSO
    }
}
