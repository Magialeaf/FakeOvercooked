using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dotText;

    private float DotRate = 0.5f;

    private void Start()
    {
        StartCoroutine(DotAnimation());
    }

    IEnumerator DotAnimation()
    {
        while (true)
        {
            int n = 0;
            dotText.text = "";
            while (n < 6)
            {
                dotText.text += ".";
                n++;
                yield return new WaitForSeconds(DotRate);
            }
        }
    }
}
