using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved;

    private const int MAX_PLATE_COUNT = 4;
    private const float PLATES_SPAWN_TIMER_MAX = 4f;

    [SerializeField] private KitchenObjectTemplate kitchenObjectTemplate;

    private float platesSpawnTimer;
    private int currentPlateCount;

    private void Start()
    {
        currentPlateCount = 0;
    }

    private void Update()
    {
        platesSpawnTimer += Time.deltaTime;

        if (platesSpawnTimer > PLATES_SPAWN_TIMER_MAX)
        {
            platesSpawnTimer = 0f;

            if(currentPlateCount < MAX_PLATE_COUNT)
            {
                currentPlateCount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (currentPlateCount > 0)
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectTemplate, player);        
                currentPlateCount--;
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
