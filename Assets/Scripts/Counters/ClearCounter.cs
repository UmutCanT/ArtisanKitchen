using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, ICanCarryKitchenObject
{
    [SerializeField] private KitchenObjectTemplete objTemp;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondOne;
    [SerializeField] bool testing;

    private KitchenObject kitchenObject;
    public KitchenObject KitchenObject { get => kitchenObject; set => kitchenObject = value; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && testing) 
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetKitchenObjectParent(secondOne);
            }
        }
    }


    public void Interact()
    {
        if (kitchenObject == null)
        {
            GameObject spawnedKitchenObject = Instantiate(objTemp.Prefab, counterTopPoint);
            spawnedKitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }       
    }

    public Transform GetParentTransform() => counterTopPoint;

    public void ClearKitchenObject() => kitchenObject = null;

    public bool HasKitchenObject() => kitchenObject != null;
}
