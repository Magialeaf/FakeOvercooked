using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // 手上有KitchenObject
        if (player.IsHaveKitchenObject())
        {
            // 手上有盘子
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {
                // 当前柜台 为空
                if (IsHaveKitchenObject() == false)
                {
                    TransferKitchenObject(player, this);
                }
                // 当前柜台 有食材
                else
                {
                    bool isSuccess = plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO());
                    if (isSuccess)
                    {
                        DestroyKitchenObject();
                    }
                }
            }
            // 手上有普通食材
            else
            {
                // 当前柜台 为空
                if (IsHaveKitchenObject() == false)
                {
                    TransferKitchenObject(player, this);
                }
                // 当前柜台 有食材
                else
                {
                    if (GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
                    {
                        if (plateKitchenObject.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject();
                        }
                    }
                }
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
            }
        }
    }
}


// public class ClearCounter : BaseCounter
// {
//     [SerializeField] private ClearCounter transferTargetCounter;
//     [SerializeField] private bool testing = false;


//     bool flag = false;

//     [SerializeField] private GameControl gameControl;
//     private void Start()
//     {
//         gameControl = new GameControl();
//         gameControl.Player.Enable();
//     }

//     private void Update()
//     {
//         Testing();
//     }
//     private void ResetFlag()
//     {
//         flag = false;
//     }

//     public void Testing()
//     {
//         float interactionValue = gameControl.Player.testing.ReadValue<float>();
//         if (testing && interactionValue > 0.5f)
//         {
//             if (flag == false)
//             {
//                 flag = true;
//                 TransferKitchenObject(this, transferTargetCounter);
//                 Invoke("ResetFlag", 1f); // 延迟1秒后重置flag
//             }
//         }
//     }
// }
