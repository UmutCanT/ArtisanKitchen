using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private GameObject iconTemplate;

    private void Awake()
    {
        iconTemplate.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateIcons();
    }
    
    private void UpdateIcons()
    {
        RemoveAllIcons();
        foreach (KitchenObjectTemplate objectTemplate in plateKitchenObject.KitchenObjectTemplates)
        {
            GameObject icon = Instantiate(iconTemplate, transform);
            icon.SetActive(true);
            icon.GetComponent<PlateIconTemplateUI>().SetKitchenObjectTemplateIcon(objectTemplate);
        }
    }

    private void RemoveAllIcons()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate.transform) continue;
            Destroy(child.gameObject);
        }
    }
}
