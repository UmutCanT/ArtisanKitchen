using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;

    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
    }

    [SerializeField] private FryingRecipe[] fryingRecipes;
    [SerializeField] private OvercookedRecipe[] overcookedRecipes;

    private State currentState;
    private FryingRecipe fryingRecipe;
    private OvercookedRecipe overcookedRecipe;
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
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = currentState
                });
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
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs 
                { 
                    state = currentState 
                });
                StartCoroutine(Fry());
                break;
            case State.Fried:
                overcookedRecipe = GetOvercookedRecipeWithInputObj(KitchenObj.ObjectTemplate);
                while (fryingTimer <= overcookedRecipe.TotalOvercookTime)
                {
                    fryingTimer += Time.deltaTime;
                    yield return null;
                }
                KitchenObj.RemovingItself();
                KitchenObject.SpawnKitchenObject(overcookedRecipe.BurnedObj, this);
                currentState = State.Burned;
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs 
                { 
                    state = currentState 
                });
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
                currentState = State.None;
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = currentState
                });
            }
        }
    }

    private bool InputObjectHasRecipe(KitchenObjectTemplate inputObj)
    {
        FryingRecipe fryingRecipe = GetFryingRecipeWithInputObj(inputObj);
        return fryingRecipe != null;
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

    private OvercookedRecipe GetOvercookedRecipeWithInputObj(KitchenObjectTemplate inputObj)
    {
        foreach (OvercookedRecipe recipe in overcookedRecipes)
        {
            if (recipe.InputObj == inputObj)
            {
                return recipe;
            }
        }
        return null;
    }

    public enum State
    {
        None,
        Frying,
        Fried,
        Burned
    }
}
