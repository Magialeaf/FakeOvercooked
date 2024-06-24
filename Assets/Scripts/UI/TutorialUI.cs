using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;

    [SerializeField] private TextMeshProUGUI upKey;
    [SerializeField] private TextMeshProUGUI downKey;
    [SerializeField] private TextMeshProUGUI leftKey;
    [SerializeField] private TextMeshProUGUI rightKey;
    [SerializeField] private TextMeshProUGUI interactKey;
    [SerializeField] private TextMeshProUGUI operationKey;
    [SerializeField] private TextMeshProUGUI pauseKey;


    private void Start()
    {
        GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
        Show();
    }
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {
        UpdateVisual();
        uiParent.SetActive(true);

    }
    private void Hide() => uiParent.SetActive(false);

    private void UpdateVisual()
    {
        upKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        downKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        leftKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        rightKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        interactKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        operationKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operation);
        pauseKey.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }
}
