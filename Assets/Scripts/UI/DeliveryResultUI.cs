using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Animator deliverySuccessUIAnimator;
    [SerializeField] private Animator deliveryFailUIAnimator;

    private const string IS_SHOW = "IsShow";

    private void Start()
    {
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
    }

    private void OrderManager_OnRecipeSuccessed(object render, System.EventArgs e)
    {
        deliverySuccessUIAnimator.gameObject.SetActive(true);
        deliverySuccessUIAnimator.SetBool(IS_SHOW, true);
    }

    private void OrderManager_OnRecipeFailed(object render, System.EventArgs e)
    {
        deliveryFailUIAnimator.gameObject.SetActive(true);
        deliveryFailUIAnimator.SetBool(IS_SHOW, true);
    }
}
