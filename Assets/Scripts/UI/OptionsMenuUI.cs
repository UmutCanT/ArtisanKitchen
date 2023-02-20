using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    private const string SFX_TEXT = "Sound Effect:";
    private const string MUSIC_TEXT = "Music:";
    private const string PLAYER_INPUTS_TAG = "Player";

    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    #region KEY_BINDINGS

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI moveUpButtonText;
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactAltButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;
    [SerializeField] private GameObject rebindPopUp;

    #endregion

    private PlayerInput playerInputs;

    private void Awake()
    {
        playerInputs = GameObject.FindGameObjectWithTag(PLAYER_INPUTS_TAG).GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        SoundManager.OnVolumeChange += SoundManager_OnVolumeChange;
        MusicManager.OnVolumeChange += MusicManager_OnVolumeChange;
        moveUpButton.onClick.AddListener(() => {RebindBinding(PlayerInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() => {RebindBinding(PlayerInput.Binding.Move_Down);});
        moveLeftButton.onClick.AddListener(() => {RebindBinding(PlayerInput.Binding.Move_Left);});
        moveRightButton.onClick.AddListener(() => {RebindBinding(PlayerInput.Binding.Move_Right);});
        interactButton.onClick.AddListener(() => {RebindBinding(PlayerInput.Binding.Interact);});
        interactAltButton.onClick.AddListener(() => {RebindBinding(PlayerInput.Binding.InteractAlternate);});
        pauseButton.onClick.AddListener(() => {RebindBinding(PlayerInput.Binding.Pause);});
    }

    private void Start()
    {
        KeyBindingsTextUpdate();
        PopUpActive(false);
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

    private void KeyBindingsTextUpdate()
    {
        moveUpButtonText.text = playerInputs.GetBindingText(PlayerInput.Binding.Move_Up);
        moveDownButtonText.text = playerInputs.GetBindingText(PlayerInput.Binding.Move_Down);
        moveLeftButtonText.text = playerInputs.GetBindingText(PlayerInput.Binding.Move_Left);
        moveRightButtonText.text = playerInputs.GetBindingText(PlayerInput.Binding.Move_Right);
        interactButtonText.text = playerInputs.GetBindingText(PlayerInput.Binding.Interact);
        interactAltButtonText.text = playerInputs.GetBindingText(PlayerInput.Binding.InteractAlternate);
        pauseButtonText.text = playerInputs.GetBindingText(PlayerInput.Binding.Pause);
    }

    public string TextUpdate (string text, float volume)
    {
        return $"{text} {Mathf.Round(volume * 10f)}";
    }

    public void UIActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void PopUpActive(bool isActive)
    {
        rebindPopUp.SetActive(isActive);
    }

    private void RebindBinding(PlayerInput.Binding binding)
    {
        PopUpActive(true);
        playerInputs.RebindBinding(binding, () =>
        {
            PopUpActive(false);
            KeyBindingsTextUpdate();
        });
    }
}
