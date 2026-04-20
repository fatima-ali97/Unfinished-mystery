using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("UI Elements")]
    public Button fullscreenButton;
    public TMP_Text fullscreenText;
    public Slider musicSlider;
    public Slider sfxSlider;
    public TMP_Dropdown resolutionDropdown;
    public Button applyButton;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private bool settingsChanged = false;

    // Saved settings
    private float savedMusic;
    private float savedSFX;
    private int savedResolution;
    private bool savedFullscreen;

    private void OnEnable()
    {
        LoadSavedSettings();
        AddListeners();
        UpdateApplyButtonState();
    }

    private void LoadSavedSettings()
    {
        // Load saved values with defaults
        savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        savedResolution = PlayerPrefs.GetInt("ResolutionIndex", Screen.currentResolution.width == 1920 ? 0 : 0);
        savedFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        // Apply to UI
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
        resolutionDropdown.value = savedResolution;
        fullscreenText.text = savedFullscreen ? "ON" : "OFF";

        // Apply to system
        ApplyAudio();
        ApplyResolution();
        Screen.fullScreen = savedFullscreen;
    }

    private void AddListeners()
    {
        fullscreenButton.onClick.RemoveAllListeners();
        fullscreenButton.onClick.AddListener(ToggleFullscreen);

        musicSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.AddListener((_) => OnSettingChanged());

        sfxSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.AddListener((_) => OnSettingChanged());

        resolutionDropdown.onValueChanged.RemoveAllListeners();
        resolutionDropdown.onValueChanged.AddListener((_) => OnSettingChanged());

        applyButton.onClick.RemoveAllListeners();
        applyButton.onClick.AddListener(ApplySettings);
    }

    private void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fullscreenText.text = Screen.fullScreen ? "ON" : "OFF";
        OnSettingChanged();
    }

    private void OnSettingChanged()
    {
        settingsChanged = true;
        UpdateApplyButtonState();
        ApplyAudio(); // instant preview while sliding
    }

    private void ApplyAudio()
    {
        if (musicSource != null)
            musicSource.volume = Mathf.Clamp01(musicSlider.value); // 0 = mute, 1 = max
        if (sfxSource != null)
            sfxSource.volume = Mathf.Clamp01(sfxSlider.value);
    }

    private void UpdateApplyButtonState()
    {
        applyButton.interactable = settingsChanged;
    }

    public void ApplySettings()
    {
        // Save settings
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();

        // Update saved values
        savedMusic = musicSlider.value;
        savedSFX = sfxSlider.value;
        savedResolution = resolutionDropdown.value;
        savedFullscreen = Screen.fullScreen;

        // Apply resolution
        ApplyResolution();

        // Disable apply button
        settingsChanged = false;
        UpdateApplyButtonState();

        Debug.Log("Settings applied perfectly!");
    }

    private void ApplyResolution()
    {
        if (resolutionDropdown.options.Count == 0) return;

        Resolution res = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void ReturnToMenu()
    {
        // Reload saved settings
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
        resolutionDropdown.value = savedResolution;
        Screen.fullScreen = savedFullscreen;
        fullscreenText.text = savedFullscreen ? "ON" : "OFF";

        ApplyAudio();
        ApplyResolution();

        settingsChanged = false;
        UpdateApplyButtonState();
    }
}