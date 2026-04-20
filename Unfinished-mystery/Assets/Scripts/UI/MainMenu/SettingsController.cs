using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("UI Elements")]
    public Button fullscreenButton;
    public TMP_Text fullscreenText;          // ON = click to activate, OFF = click to deactivate
    public Slider musicSlider;
    public Slider sfxSlider;
    public TMP_Dropdown resolutionDropdown;
    public Button applyButton;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Fixed resolution options for the dropdown
    private readonly Vector2Int[] supportedResolutions =
    {
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080)
    };

    // Saved/applied settings
    private float savedMusic;
    private float savedSFX;
    private int savedResolutionIndex;
    private bool savedFullscreen;

    // Current selected settings in UI
    private int pendingResolutionIndex;
    private bool pendingFullscreen;

    private void OnEnable()
    {
        SetupSliders();
        SetupResolutionDropdown();
        LoadSavedSettings();
        RefreshUIFromSaved();
        ApplySavedAudio();
        ApplySavedDisplay();
        AddListeners();
        UpdateApplyButtonState();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void SetupSliders()
    {
        if (musicSlider != null)
        {
            musicSlider.minValue = 0f;
            musicSlider.maxValue = 1f;
            musicSlider.wholeNumbers = false;
        }

        if (sfxSlider != null)
        {
            sfxSlider.minValue = 0f;
            sfxSlider.maxValue = 1f;
            sfxSlider.wholeNumbers = false;
        }
    }

    private void SetupResolutionDropdown()
    {
        if (resolutionDropdown == null) return;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (Vector2Int res in supportedResolutions)
        {
            options.Add($"{res.x} x {res.y}");
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    private void LoadSavedSettings()
    {
        savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        savedResolutionIndex = Mathf.Clamp(PlayerPrefs.GetInt("ResolutionIndex", 0), 0, supportedResolutions.Length - 1);
        savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 0) == 1;

        pendingResolutionIndex = savedResolutionIndex;
        pendingFullscreen = savedFullscreen;
    }

    private void RefreshUIFromSaved()
    {
        RemoveListeners();

        if (musicSlider != null) musicSlider.value = savedMusic;
        if (sfxSlider != null) sfxSlider.value = savedSFX;
        if (resolutionDropdown != null) resolutionDropdown.value = pendingResolutionIndex;

        RefreshFullscreenButtonText();

        AddListeners();
    }

    private void AddListeners()
    {
        if (fullscreenButton != null)
        {
            fullscreenButton.onClick.RemoveAllListeners();
            fullscreenButton.onClick.AddListener(ToggleFullscreenSelection);
        }

        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(OnSfxSliderChanged);
        }

        if (resolutionDropdown != null)
        {
            resolutionDropdown.onValueChanged.RemoveAllListeners();
            resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        }

        if (applyButton != null)
        {
            applyButton.onClick.RemoveAllListeners();
            applyButton.onClick.AddListener(ApplySettings);
        }
    }

    private void RemoveListeners()
    {
        if (fullscreenButton != null)
            fullscreenButton.onClick.RemoveAllListeners();

        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveAllListeners();

        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveAllListeners();

        if (resolutionDropdown != null)
            resolutionDropdown.onValueChanged.RemoveAllListeners();

        if (applyButton != null)
            applyButton.onClick.RemoveAllListeners();
    }

    private void RefreshFullscreenButtonText()
    {
        if (fullscreenText == null) return;

        // Your preferred logic:
        // if fullscreen is currently selected, button shows OFF
        // if windowed is currently selected, button shows ON
        fullscreenText.text = pendingFullscreen ? "OFF" : "ON";
    }

    private void OnMusicSliderChanged(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;

        UpdateApplyButtonState();
    }

    private void OnSfxSliderChanged(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;

        UpdateApplyButtonState();
    }

    private void OnResolutionChanged(int index)
    {
        pendingResolutionIndex = Mathf.Clamp(index, 0, supportedResolutions.Length - 1);
        UpdateApplyButtonState();
    }

    private void ToggleFullscreenSelection()
    {
        pendingFullscreen = !pendingFullscreen;
        RefreshFullscreenButtonText();
        UpdateApplyButtonState();
    }

    private void ApplySavedAudio()
    {
        if (musicSource != null)
            musicSource.volume = savedMusic;

        if (sfxSource != null)
            sfxSource.volume = savedSFX;
    }

    private void ApplySavedDisplay()
    {
        ApplyDisplay(savedResolutionIndex, savedFullscreen);
    }

    private void ApplyDisplay(int resolutionIndex, bool fullscreen)
    {
        resolutionIndex = Mathf.Clamp(resolutionIndex, 0, supportedResolutions.Length - 1);

        Vector2Int res = supportedResolutions[resolutionIndex];
        FullScreenMode mode = fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        Screen.SetResolution(res.x, res.y, mode);
    }

    private void UpdateApplyButtonState()
    {
        bool musicChanged = !Mathf.Approximately(musicSlider.value, savedMusic);
        bool sfxChanged = !Mathf.Approximately(sfxSlider.value, savedSFX);
        bool resolutionChanged = pendingResolutionIndex != savedResolutionIndex;
        bool fullscreenChanged = pendingFullscreen != savedFullscreen;

        bool hasChanges = musicChanged || sfxChanged || resolutionChanged || fullscreenChanged;

        if (applyButton != null)
            applyButton.interactable = hasChanges;
    }

    public void ApplySettings()
    {
        // Save selected values
        savedMusic = musicSlider.value;
        savedSFX = sfxSlider.value;
        savedResolutionIndex = pendingResolutionIndex;
        savedFullscreen = pendingFullscreen;

        PlayerPrefs.SetFloat("MusicVolume", savedMusic);
        PlayerPrefs.SetFloat("SFXVolume", savedSFX);
        PlayerPrefs.SetInt("ResolutionIndex", savedResolutionIndex);
        PlayerPrefs.SetInt("Fullscreen", savedFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        // Apply them now
        ApplySavedAudio();
        ApplySavedDisplay();

        UpdateApplyButtonState();

        Debug.Log("Settings applied successfully.");
    }

    public void ReturnToMenu()
    {
        // Revert UI back to saved/applied values
        pendingResolutionIndex = savedResolutionIndex;
        pendingFullscreen = savedFullscreen;

        RemoveListeners();

        if (musicSlider != null) musicSlider.value = savedMusic;
        if (sfxSlider != null) sfxSlider.value = savedSFX;
        if (resolutionDropdown != null) resolutionDropdown.value = savedResolutionIndex;

        RefreshFullscreenButtonText();

        AddListeners();

        // Re-apply saved values
        ApplySavedAudio();
        ApplySavedDisplay();

        UpdateApplyButtonState();
    }
}