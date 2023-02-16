using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{  
    [SerializeField] private DishList avaibleDishRecipes;
    
    private List<DishRecipe> waitingOrdersdishRecipes;
}
