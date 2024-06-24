using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        Hide();
        GameManager.Instance.onGamePause += GameManager_OnGamePause;
        GameManager.Instance.onGameUnpause += GameManager_OnGameUnpause;

        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ToggleGame();
        });
        settingButton.onClick.AddListener(() =>
        {
            SettingsUI.Instance.Show();
        });
        menuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameMenuScene);
        });
    }

    private void GameManager_OnGameUnpause(object sender, System.EventArgs e) => Hide();
    private void GameManager_OnGamePause(object sender, System.EventArgs e) => Show();

    public void Show() => uiParent.SetActive(true);
    public void Hide() => uiParent.SetActive(false);
}
