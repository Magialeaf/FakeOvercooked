using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateSO;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private int plateCountMax = 5;

    private List<KitchenObject> platesList = new List<KitchenObject>();

    private float timer = 0;

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            if (platesList.Count < plateCountMax)
            {
                timer += Time.deltaTime;
            }
            if (timer >= spawnRate)
            {
                timer = 0;
                SpawnPlate();
            }
        }
    }

    public override void Interact(Player player)
    {
        // 手上有食材
        if (player.IsHaveKitchenObject())
        {
            //// 当前柜台 为空
            //if (IsHaveKitchenObject() == false)
            //{
            //    TransferKitchenObject(player, this);
            //}
            //// 当前柜台 有食材
            //else
            //{

            //}
        }
        // 手上没有食材
        else
        {
            // 当前柜台 为空
            if (platesList.Count > 0)
            {
                player.AddKitchenObject(platesList[platesList.Count - 1]);
                platesList.RemoveAt(platesList.Count - 1);
            }
            // 当前柜台 有食材
            else
            {
                TransferKitchenObject(this, player);
            }
        }
    }

    public void SpawnPlate()
    {

        if (platesList.Count >= plateCountMax)
        {
            timer = 0;
            return;
        }
        KitchenObject kitchenObject = GameObject.Instantiate(plateSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();

        kitchenObject.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * platesList.Count;

        platesList.Add(kitchenObject);
    }

}
