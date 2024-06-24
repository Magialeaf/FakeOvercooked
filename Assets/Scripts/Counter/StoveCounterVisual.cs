using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject sizeelingParticles;

    public void ShowStoveEffect()
    {
        stoveOnVisual.SetActive(true);
        sizeelingParticles.SetActive(true);
    }

    public void HideStoveEffect()
    {
        stoveOnVisual.SetActive(false);
        sizeelingParticles.SetActive(false);
    }
}
