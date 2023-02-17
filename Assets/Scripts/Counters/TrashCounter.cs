using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectThrashed;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.KitchenObj.RemovingItself();

            OnAnyObjectThrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
