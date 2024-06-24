using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }

    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    [SerializeField] private Button closeButton;

    [SerializeField] private Button upKeyButton;
    [SerializeField] private Button downKeyButton;
    [SerializeField] private Button leftKeyButton;
    [SerializeField] private Button rightKeyButton;
    [SerializeField] private Button interactKeyButton;
    [SerializeField] private Button operateKeyButton;
    [SerializeField] private Button pauseKeyButton;

    [SerializeField] private TextMeshProUGUI upKeyText;
    [SerializeField] private TextMeshProUGUI downKeyText;
    [SerializeField] private TextMeshProUGUI leftKeyText;
    [SerializeField] private TextMeshProUGUI rightKeyText;
    [SerializeField] private TextMeshProUGUI interactKeyText;
    [SerializeField] private TextMeshProUGUI operateKeyText;
    [SerializeField] private TextMeshProUGUI pauseKeyText;

    [SerializeField] GameObject reindingUI;

    private void Start()
    {
        Instance = this;

        UpdateVisual();
        Hide();

        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        upKeyButton.onClick.AddListener(() => Reinding(GameInput.BindingType.Up));
        downKeyButton.onClick.AddListener(() => Reinding(GameInput.BindingType.Down));
        leftKeyButton.onClick.AddListener(() => Reinding(GameInput.BindingType.Left));
        rightKeyButton.onClick.AddListener(() => Reinding(GameInput.BindingType.Right));
        interactKeyButton.onClick.AddListener(() => Reinding(GameInput.BindingType.Interact));
        operateKeyButton.onClick.AddListener(() => Reinding(GameInput.BindingType.Operation));
        pauseKeyButton.onClick.AddListener(() => Reinding(GameInput.BindingType.Pause));
    }

    public void Show() => uiParent.SetActive(true);
    public void Hide() => uiParent.SetActive(false);

    public void UpdateVisual()
    {
        soundButtonText.text = "音效大小：" + SoundManager.Instance.GetVolume().ToString();
        musicButtonText.text = "音乐大小：" + MusicManager.Instance.GetVolume().ToString();

        upKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        downKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        leftKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        rightKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        interactKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        operateKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operation);
        pauseKeyText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }

    private void Reinding(GameInput.BindingType bindingType)
    {
        reindingUI.SetActive(true);
        GameInput.Instance.ReBinding(bindingType, () =>
        {
            UpdateVisual();
            reindingUI.SetActive(false);
        });
    }
}
