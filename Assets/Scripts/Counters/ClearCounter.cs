using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectTemplete objTemp;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact()
    {
        if (kitchenObject == null)
        {
            GameObject spawnedKitchenObject = Instantiate(objTemp.Prefab, counterTopPoint);
            spawnedKitchenObject.transform.localPosition = Vector3.zero;
 
            kitchenObject = spawnedKitchenObject.GetComponent<KitchenObject>();
        }       
    }
}
