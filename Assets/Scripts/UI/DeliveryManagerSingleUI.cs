using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO _recipeSO)
    {
        recipeNameText.text = _recipeSO.recipeName;

        foreach( Transform child in iconContainer)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectsSO kitchenObjectsSO in _recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            // Image
            iconTransform.GetComponent<Image>().sprite = kitchenObjectsSO.sprite;
        }
    }
}
