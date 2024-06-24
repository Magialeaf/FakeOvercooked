using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler onStateChanged;
    public event EventHandler onGamePause;
    public event EventHandler onGameUnpause;

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] Player player;

    private State state;

    // private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer = 15f;
    private float gamePlayingTimerTotal;

    private bool isGamePause = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TurnToWaitingToStart();
        gamePlayingTimerTotal = gamePlayingTimer;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                //waitingToStartTimer -= Time.deltaTime;
                //if (waitingToStartTimer <= 0f)
                //{
                //    TurnToCountDownToStart();
                //}
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0f)
                {
                    TurnToGamePlaying();
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    TurnToGameOver();
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }

    private void TurnToWaitingToStart()
    {
        state = State.WaitingToStart;
        DisablePlayer();
        onStateChanged?.Invoke(this, EventArgs.Empty);
        // 新加代码
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }
    private void TurnToCountDownToStart()
    {
        // 新加代码
        GameInput.Instance.OnInteractAction -= GameInput_OnInteractAction;

        state = State.CountDownToStart;
        DisablePlayer();
        onStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        onStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurnToGameOver()
    {
        state = State.GameOver;
        DisablePlayer();
        onStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DisablePlayer()
    {
        player.enabled = false;
    }
    private void EnablePlayer()
    {
        player.enabled = true;
    }

    public bool IsWaitingToStart() => state == State.WaitingToStart;
    public bool IsCountDownToStart() => state == State.CountDownToStart;
    public bool IsGamePlaying() => state == State.GamePlaying;
    public bool IsGameOver() => state == State.GameOver;


    public float GetCountDownToStartTimer() => countDownToStartTimer;

    public void ToggleGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0f;
            onGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            onGameUnpause?.Invoke(this, EventArgs.Empty);
        }
    }

    public float GetGamePlayingTimer() => gamePlayingTimer;
    public float GetGamePlayingTimerNormalized() => gamePlayingTimer / gamePlayingTimerTotal;

    public void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        TurnToCountDownToStart();
    }

    public void Restart()
    {
        Loader.Load(Loader.Scene.GameScene);
    }
}