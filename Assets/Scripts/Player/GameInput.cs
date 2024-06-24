using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private const string GAME_INPUT_BINDINGS = "GameInputBindings";

    public event EventHandler OnInteractAction;
    public event EventHandler OnOperationAction;
    public event EventHandler OnPauseAction;

    private GameControl gameControl;

    public enum BindingType
    {
        Up,
        Down,
        Left,
        Right,
        Interact,
        Operation,
        Pause
    }

    public void Awake()
    {
        Instance = this;
        gameControl = new GameControl();
        if (PlayerPrefs.HasKey(GAME_INPUT_BINDINGS))
        {
            // 重新绑定信息
            gameControl.LoadBindingOverridesFromJson(PlayerPrefs.GetString(GAME_INPUT_BINDINGS));
        }
        gameControl.Player.Enable();

        gameControl.Player.Interact.performed += Interact_Performed;
        gameControl.Player.Operation.performed += Operation_Performed;
        gameControl.Player.Pause.performed += Pause_Performed;
    }

    private void OnDestroy()
    {
        gameControl.Player.Interact.performed -= Interact_Performed;
        gameControl.Player.Operation.performed -= Operation_Performed;
        gameControl.Player.Pause.performed -= Pause_Performed;

        gameControl.Dispose();
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => OnInteractAction?.Invoke(this, EventArgs.Empty);
    private void Operation_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => OnOperationAction?.Invoke(this, EventArgs.Empty);
    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => OnPauseAction?.Invoke(this, EventArgs.Empty);


    public Vector3 GetMovementDirectionNormalized()
    {
        // 获得对应actions值gameControl.[mapName].[actionsName].Enable()
        Vector2 inputVector2 = gameControl.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputVector2.x, 0, inputVector2.y);

        direction = direction.normalized;

        return direction;
    }

    public void ReBinding(BindingType bindingType, Action onComplete)
    {
        InputAction inputAction = null;
        int index = -1;
        switch (bindingType)
        {
            case BindingType.Up:
                index = 1;
                inputAction = gameControl.Player.Move;
                break;
            case BindingType.Down:
                index = 2;
                inputAction = gameControl.Player.Move;
                break;
            case BindingType.Left:
                index = 3;
                inputAction = gameControl.Player.Move;
                break;
            case BindingType.Right:
                index = 4;
                inputAction = gameControl.Player.Move;
                break;
            case BindingType.Interact:
                index = 0;
                inputAction = gameControl.Player.Interact;
                break;
            case BindingType.Operation:
                index = 0;
                inputAction = gameControl.Player.Operation;
                break;
            case BindingType.Pause:
                index = 0;
                inputAction = gameControl.Player.Pause;
                break;
            default:
                break;
        }

        gameControl.Player.Disable();
        inputAction.PerformInteractiveRebinding(index).OnComplete(callback =>
        {
            callback.Dispose();
            gameControl.Player.Enable();
            onComplete?.Invoke();

            // gameControl.SaveBindingOverridesAsJson(); 获得绑定的JSON数据（不会保存）
            PlayerPrefs.SetString(GAME_INPUT_BINDINGS, gameControl.SaveBindingOverridesAsJson());
            // 手动保存（不手动也一样会自动保存，但是只有当数量达到一定值以后才会自动保存）
            PlayerPrefs.Save();
        }).Start();
    }

    public string GetBindingDisplayString(BindingType bindingType)
    {
        switch (bindingType)
        {
            case BindingType.Up:
                return gameControl.Player.Move.bindings[1].ToDisplayString();
            case BindingType.Down:
                return gameControl.Player.Move.bindings[2].ToDisplayString();
            case BindingType.Left:
                return gameControl.Player.Move.bindings[3].ToDisplayString();
            case BindingType.Right:
                return gameControl.Player.Move.bindings[4].ToDisplayString();
            case BindingType.Interact:
                return gameControl.Player.Interact.bindings[0].ToDisplayString();
            case BindingType.Operation:
                return gameControl.Player.Operation.bindings[0].ToDisplayString();
            case BindingType.Pause:
                return gameControl.Player.Pause.bindings[0].ToDisplayString();
            default:
                return string.Empty;
        }
    }
}