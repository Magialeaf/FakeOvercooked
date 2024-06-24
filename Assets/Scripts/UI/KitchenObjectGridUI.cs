using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectGridUI : MonoBehaviour
{
    [SerializeField] private IconTemplateUI iconTemplateUI;

    private void Start()
    {
        iconTemplateUI.Hide();
    }

    public void ShowKitchenObjectUI(KitchenObjectSO kitchenObjectSO)
    {
        IconTemplateUI newIconTemplateUI = GameObject.Instantiate(iconTemplateUI, transform);
        newIconTemplateUI.Show(kitchenObjectSO.sprite);
    }
}
