﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private ContainerCounterVisual visual;

    public override void Interact(Player player)
    {

        if (player.IsHaveKitchenObject()) { return; }

        CreateKitchenObject(kitchenObjectSO.prefab);
        TransferKitchenObject(this, player);

        visual.PlayOpen();

    }
}
