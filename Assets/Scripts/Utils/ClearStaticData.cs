using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStaticData : MonoBehaviour
{
    void Start()
    {
        TrashCounter.ClearStaticData();
        KitchenObjectHolder.ClearStaticData();
        CuttingCounter.ClearStaticData();
    }
}
