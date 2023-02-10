using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectTemplete objectTemplete;

    private ClearCounter clearCounter;

    public KitchenObjectTemplete ObjectTemplete => objectTemplete;

    public void ChangeClearCounter(ClearCounter clearCounter) 
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;
        clearCounter.KitchenObject = this;
        transform.parent = clearCounter.CounterTopPoint;
        transform.localPosition = Vector3.zero;
    }
}
