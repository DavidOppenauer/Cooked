using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetKitchenObjectSO(KitchenObjectsSO _kitchenObjectSO)
    {
        image.sprite = _kitchenObjectSO.sprite;
    }
}
