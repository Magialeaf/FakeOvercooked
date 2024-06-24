using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private const float DEFAULT_VOLUME = 0.2f;
    private const string SOUND_MANAGER_VOLUME = "SoundManagerVolume";

    private int userVolume = 5;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        LoadVolume();
    }

    private void Start()
    {
        // 单例每次在切换场景时自动销毁，里面的实例事件也一并销毁，不需要额外处理
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;

        // 静态事件在切换场景时不会销毁，所以需要在某些地方手动销毁（比如在开始的菜单界面）
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
        KitchenObjectHolder.OnPickUp += KitchenObjectHolder_OnPickUp;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }



    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e) => PlaySound(audioClipRefsSO.deliverySuccess);
    private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e) => PlaySound(audioClipRefsSO.deliveryFail);

    private void CuttingCounter_OnCut(object sender, System.EventArgs e) => PlaySound(audioClipRefsSO.chop);
    private void KitchenObjectHolder_OnDrop(object sender, System.EventArgs e) => PlaySound(audioClipRefsSO.objectDrop);
    private void KitchenObjectHolder_OnPickUp(object sender, System.EventArgs e) => PlaySound(audioClipRefsSO.objectPickup);
    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e) => PlaySound(audioClipRefsSO.trash);


    public void PlayWarningSound() => PlaySound(audioClipRefsSO.warning);
    public void PlayCountDownSound() => PlaySound(audioClipRefsSO.warning);
    public void PlayStepSound(float Volume = DEFAULT_VOLUME) => PlaySound(audioClipRefsSO.footstep, Volume);




    private void PlaySound(AudioClip[] clips, float Volume = DEFAULT_VOLUME)
    {
        PlaySound(clips, Camera.main.transform.position, Volume);
    }

    private void PlaySound(AudioClip[] clips, Vector3 positions, float Volume = DEFAULT_VOLUME)
    {
        if (userVolume == 0) return;

        int index = Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[index], positions, Volume * (userVolume / 10.0f));
    }

    public void ChangeVolume()
    {
        userVolume = (userVolume + 1) % 11;
        SaveVolume();
    }

    public int GetVolume() => userVolume;

    public float GetAudioVolume(AudioSource sound) => sound.volume * (userVolume / 10.0f);

    private void SaveVolume() => PlayerPrefs.SetInt(SOUND_MANAGER_VOLUME, userVolume);
    private void LoadVolume() => userVolume = PlayerPrefs.GetInt(SOUND_MANAGER_VOLUME, userVolume);

}
