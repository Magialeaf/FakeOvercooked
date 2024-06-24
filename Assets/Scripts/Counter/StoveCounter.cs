using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeList fryingRecipeList;
    [SerializeField] private FryingRecipeList burningRecipeList;

    [SerializeField] private StoveCounterVisual stoveCounterVisual;

    [SerializeField] private ProgressBarUI progressBarUI;

    [SerializeField] private AudioSource sound;
    private WarningControl warningControl;

    public enum StoveState
    {
        Idle,
        Frying,
        Burning
    }

    private FryingRecipe fryingRecipe;
    private float fryingTimer = 0f;
    private StoveState state = StoveState.Idle;
    private float warningTimenNormalize = 0.5f;

    private void Start()
    {
        warningControl = GetComponent<WarningControl>();
    }


    public override void Interact(Player player)
    {
        // 手上有食材
        if (player.IsHaveKitchenObject())
        {
            // 当前柜台 为空
            if (IsHaveKitchenObject() == false && fryingRecipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe fryingRecipe))
            {
                TransferKitchenObject(player, this);
                StartFrying(fryingRecipe);
            }
            else if (burningRecipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe burningRecipe))
            {
                TransferKitchenObject(player, this);
                StartBurning(burningRecipe);
            }
            // 当前柜台 有食材
            else
            {

            }
        }
        // 手上没有食材
        else
        {
            // 当前柜台 为空
            if (IsHaveKitchenObject() == false)
            {
            }
            // 当前柜台 有食材
            else
            {
                TurnToIdle();
                TransferKitchenObject(this, player);
            }
        }
    }

    private void Update()
    {
        switch (state)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTimer / fryingRecipe.fryingTime);

                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);

                    burningRecipeList.TryGetFryingRecipe(GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe newFryingRecipe);
                    StartBurning(newFryingRecipe);
                }
                break;
            case StoveState.Burning:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTimer / fryingRecipe.fryingTime);

                if (fryingTimer / fryingRecipe.fryingTime >= warningTimenNormalize)
                {
                    warningControl.ShowWarning();
                }

                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    TurnToIdle();
                }
                break;
            default: break;
        }
    }

    private void StartFrying(FryingRecipe fryingRecipe)
    {
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Frying;

        sound.volume = SoundManager.Instance.GetAudioVolume(sound);
        sound.Play();
        stoveCounterVisual.ShowStoveEffect();
    }

    private void StartBurning(FryingRecipe fryingRecipe)
    {
        if (fryingRecipe == null)
        {
            Debug.LogWarning("无法获得Burning的食谱！");
            TurnToIdle();
            return;
        }

        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Burning;

        sound.Play();
        stoveCounterVisual.ShowStoveEffect();
    }

    private void TurnToIdle()
    {
        state = StoveState.Idle;
        stoveCounterVisual.HideStoveEffect();
        progressBarUI.Hide();

        sound.Pause();
        warningControl.StopWarning();
    }
}
