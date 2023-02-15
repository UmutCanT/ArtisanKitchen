using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, ICanProgress
{
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public event EventHandler<ICanProgress.OnProgressChangedEventArgs> OnProgressChanged;

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
                if (player.KitchenObj.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(KitchenObj.ObjectTemplate))
                    {
                        KitchenObj.RemovingItself();
                        ResetFryingProcess();
                    }
                }
            }
            else
            {
                KitchenObj.SetKitchenObjectParent(player);
                ResetFryingProcess();
            }
        }
    }

    private void ResetFryingProcess()
    {
        StopAllCoroutines();
        currentState = State.None;
        OnProgressChanged?.Invoke(this, new ICanProgress.OnProgressChangedEventArgs
        {
            progressNormalized = 0f
        });
        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
        {
            state = currentState
        });
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
                    OnProgressChanged?.Invoke(this, new ICanProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)fryingTimer / fryingRecipe.TotalFryingTime
                    });
                    yield return null;
                }
                KitchenObj.RemovingItself();
                KitchenObject.SpawnKitchenObject(fryingRecipe.FryedObj, this);
                currentState = State.Fried;
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
                    OnProgressChanged?.Invoke(this, new ICanProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)fryingTimer / overcookedRecipe.TotalOvercookTime
                    });
                    yield return null;
                }
                KitchenObj.RemovingItself();
                KitchenObject.SpawnKitchenObject(overcookedRecipe.BurnedObj, this);
                currentState = State.Burned;
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = currentState
                });
                OnProgressChanged?.Invoke(this, new ICanProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
                break;
            case State.Burned:
                break;
            default:
                break;
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
