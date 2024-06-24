using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [System.Serializable]
    public class KitchenObjectSO_Model
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject model;
    }

    [SerializeField] private List<KitchenObjectSO_Model> modelMap;

    public void ShowKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO_Model item in modelMap)
        {
            if (item.KitchenObjectSO == kitchenObjectSO)
            {
                item.model.SetActive(true);
                return;
            }
        }
    }
}
