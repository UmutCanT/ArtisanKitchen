using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnOrderRecived;
    public event EventHandler OnOrderCompleted;

    public static DeliveryManager Instance { get; private set; }

    private const float ORDER_INTERVAL = 4f;
    private const int MAX_ORDER_COUNT = 5;

    [SerializeField] private DishList avaibleDishRecipes;
    
    private List<DishRecipe> waitingOrdersDishRecipes;
    private float orderSpawnTimer;

    public List<DishRecipe> WaitingOrdersDishRecipes => waitingOrdersDishRecipes;

    private void Awake()
    {
        Instance = this;
        waitingOrdersDishRecipes= new List<DishRecipe>();
    }

    private void Update()
    {
        orderSpawnTimer -= Time.deltaTime;
        if (orderSpawnTimer <= 0f)
        {
            orderSpawnTimer = ORDER_INTERVAL;

            if (waitingOrdersDishRecipes.Count < MAX_ORDER_COUNT)
            {
                DishRecipe newOrder = avaibleDishRecipes.DishRecipeList[Random.Range(0, avaibleDishRecipes.DishRecipeList.Count)];
                waitingOrdersDishRecipes.Add(newOrder);
                OnOrderRecived?.Invoke(this, EventArgs.Empty);
            }            
        }
    }

    public void DeliverOrder(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingOrdersDishRecipes.Count; i++)
        {
            DishRecipe waitingOrder = waitingOrdersDishRecipes[i];

            if (waitingOrder.DishIngredients.Count == plateKitchenObject.KitchenObjectTemplates.Count)
            {
                bool correctOrder = true;

                foreach (KitchenObjectTemplate orderIngredients in waitingOrder.DishIngredients)
                {
                    bool foundIngredient = false;
                    foreach (KitchenObjectTemplate plateIngredients in plateKitchenObject.KitchenObjectTemplates)
                    {
                        if (orderIngredients == plateIngredients)
                        {
                            foundIngredient = true;
                            break;
                        }
                    }

                    if (!foundIngredient)
                    {
                        correctOrder = false;
                    }
                }

                if (correctOrder)
                {
                    waitingOrdersDishRecipes.RemoveAt(i);
                    OnOrderCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
    }
}
