using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
     
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectTemplate kitchenObjectTemplate;
    }

    [SerializeField] private List<KitchenObjectTemplate> validKitchenObjTemplates;

    private List<KitchenObjectTemplate> kitchenObjectTemplates;

    public List<KitchenObjectTemplate> KitchenObjectTemplates => kitchenObjectTemplates;

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
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs 
        { 
            kitchenObjectTemplate = kitchenObjectTemplate 
        });
        return true;
    }
}
