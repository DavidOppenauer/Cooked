using UnityEngine;

// Without this simple Object id have to make a hundred thousand scripts for cheese tomato and whatever

// Not a monobehaviour! Inherints from Scriptable Objects,
// These are useful for stuff like Armors Weapons Ingredients, anything that shares behaviour essentially

// To create a scriptable Objectyou need the following line which is an atribute?? I guess its SO specific
[CreateAssetMenu()]
public class KitchenObjectsSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}