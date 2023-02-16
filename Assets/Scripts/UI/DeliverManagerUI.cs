using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject ordersUI;
    [SerializeField] private GameObject orderUITemplate;

    private void Awake()
    {
        orderUITemplate.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnOrderRecived += DeliveryManager_OnOrderRecived;
        DeliveryManager.Instance.OnOrderCompleted += DeliveryManager_OnOrderCompleted;
        RemoveAllOrdersUI();
    }

    private void DeliveryManager_OnOrderCompleted(object sender, System.EventArgs e)
    {
        UpdateOrdersUI();
    }

    private void DeliveryManager_OnOrderRecived(object sender, System.EventArgs e)
    {
        UpdateOrdersUI();
    }

    private void UpdateOrdersUI()
    {
        RemoveAllOrdersUI();
        foreach (DishRecipe dish in DeliveryManager.Instance.WaitingOrdersDishRecipes)
        {
            GameObject orderUI = Instantiate(orderUITemplate, ordersUI.transform);
            orderUI.SetActive(true);
            orderUI.GetComponent<OrderTemplateUI>().SetOrderName(dish);
        }
    }

    private void RemoveAllOrdersUI()
    {
        foreach (Transform child in ordersUI.transform)
        {
            if (child == orderUITemplate.transform) continue;
            Destroy(child.gameObject);
        }
    }
    
}
