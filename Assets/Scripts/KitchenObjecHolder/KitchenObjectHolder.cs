using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;

    public static event EventHandler OnDrop;
    public static event EventHandler OnPickUp;

    private KitchenObject kitchenObject;

    public KitchenObject GetKitchenObject() => kitchenObject;
    public KitchenObjectSO GetKitchenObjectSO() => kitchenObject.GetKitchenObjectSO();

    public bool IsHaveKitchenObject() => kitchenObject != null;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        if (this.kitchenObject != kitchenObject && kitchenObject != null && this is BaseCounter)
        {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
        else if (this.kitchenObject != kitchenObject && kitchenObject != null && this is Player)
        {
            OnPickUp?.Invoke(this, EventArgs.Empty);
        }

        this.kitchenObject = kitchenObject;
        kitchenObject.transform.localPosition = Vector3.zero;

    }

    public Transform GetHoldPoint() { return holdPoint; }

    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (sourceHolder.GetKitchenObject() == null)
        {
            Debug.LogWarning("源持有者没有可转移的厨房对象。");
            return;
        }
        if (targetHolder.GetKitchenObject() != null)
        {
            Debug.LogWarning("目标持有者已占用，无法放置更多厨房对象。");
            return;
        }
        targetHolder.AddKitchenObject(sourceHolder.GetKitchenObject());
        sourceHolder.ClearKitchenObject();
    }

    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject.transform.SetParent(holdPoint);
        SetKitchenObject(kitchenObject);
    }

    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }

    public void ClearKitchenObject() { this.kitchenObject = null; }

    public void DestroyKitchenObject()
    {
        Destroy(kitchenObject.gameObject);
        ClearKitchenObject();
    }

    public static void ClearStaticData()
    {
        OnDrop = null;
        OnPickUp = null;
    }
}
