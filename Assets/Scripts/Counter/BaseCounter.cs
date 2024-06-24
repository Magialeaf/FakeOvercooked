using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject selectedCounter;

    public virtual void Interact(Player player)
    {
        Debug.LogWarning("交互方法未被重写！");
    }

    public virtual void InteractOperation(Player player) { }

    public void SelectCounter() => selectedCounter.SetActive(true);
    public void CancelCounter() => selectedCounter.SetActive(false);
}