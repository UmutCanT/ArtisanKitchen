using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipe[] fryingRecipes;

    private State currentState;
    private FryingRecipe fryingRecipe;
    private float fryingTimer;

    private void Start()
    {
        currentState = State.None;
    }

    private IEnumerator Fry()
    {
        fryingRecipe = GetFryingRecipeWithInputObj(KitchenObj.ObjectTemplate);
        fryingTimer = 0;
        switch (currentState)
        {
            case State.None:
                currentState = State.Frying;               
                StartCoroutine(Fry());
                break;
            case State.Frying:
                while (fryingTimer <= fryingRecipe.TotalFryingTime)
                {
                    fryingTimer += Time.deltaTime;
                    yield return null;
                }
                KitchenObj.RemovingItself();
                KitchenObject.SpawnKitchenObject(fryingRecipe.FryedObj, this);
                currentState= State.Fried;
                StartCoroutine(Fry());
                break;
            case State.Fried:
                while (fryingTimer <= fryingRecipe.TotalFryingTime)
                {
                    fryingTimer += Time.deltaTime;
                    yield return null;
                }
                KitchenObj.RemovingItself();
                KitchenObject.SpawnKitchenObject(fryingRecipe.FryedObj, this);
                currentState = State.Burned;
                break;
            case State.Burned:
                break;
            default:
                break;
        }
        
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (InputObjectHasRecipe(player.KitchenObj.ObjectTemplate))
                {
                    player.KitchenObj.SetKitchenObjectParent(this);                                     
                    StartCoroutine(Fry());
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                KitchenObj.SetKitchenObjectParent(player);
                StopAllCoroutines();
                currentState = StateDecider();
            }
        }
    }

    private State StateDecider()
    {
        if (currentState == State.Fried)
            return State.Fried;
        return State.None;
    }

    private bool InputObjectHasRecipe(KitchenObjectTemplate inputObj)
    {
        FryingRecipe fryingRecipe = GetFryingRecipeWithInputObj(inputObj);
        return fryingRecipe != null;
    }

    private KitchenObjectTemplate GetFryedKitchenObject(KitchenObjectTemplate inputObj)
    {
        FryingRecipe fryingRecipe = GetFryingRecipeWithInputObj(inputObj);
        if (fryingRecipe != null)
        {
            return fryingRecipe.FryedObj;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipe GetFryingRecipeWithInputObj(KitchenObjectTemplate inputObj)
    {
        foreach (FryingRecipe recipe in fryingRecipes)
        {
            if (recipe.InputObj == inputObj)
            {
                return recipe;
            }
        }
        return null;
    }

    private enum State
    {
        None,
        Frying,
        Fried,
        Burned
    }
}
