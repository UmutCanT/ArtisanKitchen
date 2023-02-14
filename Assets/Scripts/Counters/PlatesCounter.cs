using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;

    private const int MAX_PLATES_COUNT = 4;
    private const float PLATES_SPAWN_TIMER_MAX = 4f;

    [SerializeField] private KitchenObjectTemplate kitchenObjectTemplate;

    private float platesSpawnTimer;
    private int currentPlatesCount;

    public int CurrentPlatesCount => currentPlatesCount;

    private void Start()
    {
        currentPlatesCount = 0;
    }

    private void Update()
    {
        platesSpawnTimer += Time.deltaTime;

        if (platesSpawnTimer > PLATES_SPAWN_TIMER_MAX)
        {
            platesSpawnTimer = 0f;

            if(currentPlatesCount < MAX_PLATES_COUNT)
            {
                //KitchenObject.SpawnKitchenObject(kitchenObjectTemplate, this);        
                currentPlatesCount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                KitchenObj.SetKitchenObjectParent(player);
                currentPlatesCount--;
            }
        }      
    }
}
