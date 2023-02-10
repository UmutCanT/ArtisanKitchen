using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectTemplete objectTemplete;

    public KitchenObjectTemplete ObjectTemplete { get => objectTemplete; }
}
