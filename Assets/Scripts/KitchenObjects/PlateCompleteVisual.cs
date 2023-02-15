using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectTemplateWithObject
    {
        public KitchenObjectTemplate kitchenObjectTemplate;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectTemplateWithObject> kitchenObjectTemplateWithObjectList;
    

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        DisableAllObjects();
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(KitchenObjectTemplateWithObject templateWithObject in kitchenObjectTemplateWithObjectList)
        {
            if (templateWithObject.kitchenObjectTemplate == e.kitchenObjectTemplate)
            {
                templateWithObject.gameObject.SetActive(true);
            }
        }
    }

    private void DisableAllObjects()
    {
        foreach (KitchenObjectTemplateWithObject templateWithObject in kitchenObjectTemplateWithObjectList)
        {
            templateWithObject.gameObject.SetActive(false);
        }
    }
}
