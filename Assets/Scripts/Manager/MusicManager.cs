using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource musicSource;

    private const string MUSIC_MANAGER_VOLUME = "MusicManagerVolume";

    private float originalVolume;
    private int userVolume = 5;

    void Awake()
    {
        Instance = this;
        LoadVolume();
    }

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        originalVolume = musicSource.volume;

        UpdateVolume();

        musicSource.Play();
    }

    public void ChangeVolume()
    {
        userVolume = (userVolume + 1) % 11;

        UpdateVolume();
        SaveVolume();
    }

    private void UpdateVolume()
    {
        if (userVolume == 0)
        {
            musicSource.enabled = false;
        }
        else
        {
            musicSource.enabled = true;
            musicSource.volume = originalVolume * (userVolume / 10f);
        }
    }

    public int GetVolume() => userVolume;

    public void SaveVolume() => PlayerPrefs.SetInt(MUSIC_MANAGER_VOLUME, userVolume);
    public void LoadVolume() => userVolume = PlayerPrefs.GetInt(MUSIC_MANAGER_VOLUME, userVolume);
}
