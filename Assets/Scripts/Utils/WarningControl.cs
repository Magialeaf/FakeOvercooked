using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningControl : MonoBehaviour
{
    private const string IS_FLICKER = "IsFlicker";

    [SerializeField] private GameObject warningUI;
    [SerializeField] private Animator progressBarAnimator;

    private bool isWarning = false;
    private float warningSoundRate = 0.2f;
    private float warningSoundTimer = 0f;

    private void Update()
    {
        if (isWarning)
        {
            warningSoundTimer += Time.deltaTime;
            if (warningSoundTimer >= warningSoundRate)
            {
                warningSoundTimer = 0f;
                SoundManager.Instance.PlayWarningSound();
            }
        }
    }

    public void ShowWarning()
    {
        if (!isWarning)
        {
            isWarning = true;
            warningUI.SetActive(true);
            progressBarAnimator.SetBool(IS_FLICKER, true);
        }
    }

    public void StopWarning()
    {
        if (isWarning)
        {
            isWarning = false; warningUI.SetActive(false);
            progressBarAnimator.SetBool(IS_FLICKER, false);
        }
    }
}
