using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeList cuttingRecipeList;

    [SerializeField] private ProgressBarUI progressBarUI;

    [SerializeField] private CuttingCounterVisual cuttingCounterVisual;

    public static event EventHandler OnCut;

    private int cuttingCount = 0;

    public override void Interact(Player player)
    {
        // 手上有食材
        if (player.IsHaveKitchenObject())
        {
            // 当前柜台 为空
            if (IsHaveKitchenObject() == false)
            {
                cuttingCount = 0;
                TransferKitchenObject(player, this);
            }
            // 当前柜台 有食材
            else
            {

            }
        }
        // 手上没有食材
        else
        {
            // 当前柜台 为空
            if (IsHaveKitchenObject() == false)
            {
            }
            // 当前柜台 有食材
            else
            {
                TransferKitchenObject(this, player);
                progressBarUI.Hide();
            }
        }
    }

    public override void InteractOperation(Player player)
    {
        if (IsHaveKitchenObject())
        {
            if (cuttingRecipeList.TryGetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO(), out CuttingRecipe cuttingRecipe))
            {
                Cut();

                progressBarUI.UpdateProgress((float)cuttingCount / cuttingRecipe.cuttingCount);

                if (cuttingCount == cuttingRecipe.cuttingCount)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(cuttingRecipe.output.prefab);
                }
            }
        }
    }

    public void Cut()
    {
        OnCut?.Invoke(this, EventArgs.Empty);
        cuttingCount++;
        cuttingCounterVisual.PlayCut();
    }

    public static new void ClearStaticData()
    {
        OnCut = null;
    }
}
