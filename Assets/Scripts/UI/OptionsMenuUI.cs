using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    private const string SFX_TEXT = "Sound Effect:";
    private const string MUSIC_TEXT = "Music:";

    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void OnEnable()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        SoundManager.OnVolumeChange += SoundManager_OnVolumeChange;
        MusicManager.OnVolumeChange += MusicManager_OnVolumeChange;       
    }

    private void Start()
    {
        UIActive(false);
    }

    private void OnDestroy()
    {
        SoundManager.OnVolumeChange -= SoundManager_OnVolumeChange;
        MusicManager.OnVolumeChange -= MusicManager_OnVolumeChange;
    }

    private void MusicManager_OnVolumeChange(object sender, MusicManager.OnVolumeChangeEventArgs e)
    {
        musicText.text = TextUpdate(MUSIC_TEXT, e.volume);
    }

    private void SoundManager_OnVolumeChange(object sender, SoundManager.OnVolumeChangeEventArgs e)
    {
        sfxText.text = TextUpdate(SFX_TEXT, e.volume);
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        UIActive(false);
    }

    public string TextUpdate (string text, float volume)
    {
        return $"{text} {Mathf.Round(volume * 10f)}";
    }

    public void UIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
