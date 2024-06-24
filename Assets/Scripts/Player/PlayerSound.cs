using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float stepSoundRate = 0.18f;
    private float stepSoundTimer = 0f;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        stepSoundTimer += Time.deltaTime;
        if (stepSoundTimer >= stepSoundRate)
        {
            stepSoundTimer = 0f;
            if (player.IsWalking)
            {
                float volume = 0.5f;
                SoundManager.Instance.PlayStepSound(volume);
            }
        }
    }
}
