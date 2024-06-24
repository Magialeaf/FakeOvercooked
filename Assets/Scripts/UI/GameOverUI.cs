using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Button againButton;

    private void Start()
    {
        Hide();
        GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
        againButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Restart();
        });
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
        }
    }

    public void Show()
    {
        numberText.text = OrderManager.Instance.GetSuccessDeliveryCount().ToString();
        uiParent.SetActive(true);
    }
    public void Hide() => uiParent.SetActive(false);
}
