using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectTemplate> validKitchenObjTemplates;

    private List<KitchenObjectTemplate> kitchenObjectTemplates;

    private void Awake()
    {
        kitchenObjectTemplates = new List<KitchenObjectTemplate>();    
    }

    public bool TryAddIngredient(KitchenObjectTemplate kitchenObjectTemplate)
    {
        if (!validKitchenObjTemplates.Contains(kitchenObjectTemplate))
            return false;
    
        if (kitchenObjectTemplates.Contains(kitchenObjectTemplate))
            return false;

        kitchenObjectTemplates.Add(kitchenObjectTemplate);
        return true;
    }
}
