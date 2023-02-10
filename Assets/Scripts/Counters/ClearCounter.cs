using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectTemplete objTemp;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondOne;
    [SerializeField] bool testing;
    private KitchenObject kitchenObject;

    public Transform CounterTopPoint => counterTopPoint;

    public KitchenObject KitchenObject { get => kitchenObject; set => kitchenObject = value; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && testing) 
        {
            if(kitchenObject != null)
            {
                kitchenObject.ChangeClearCounter(secondOne);
            }
        }
    }


    public void Interact()
    {
        if (kitchenObject == null)
        {
            GameObject spawnedKitchenObject = Instantiate(objTemp.Prefab, counterTopPoint);
            spawnedKitchenObject.GetComponent<KitchenObject>().ChangeClearCounter(this);
        }       
    }

    public void ClearKitchenObject() => kitchenObject = null;

    public bool HasKitchenObjectOnIt() => kitchenObject != null;
}
