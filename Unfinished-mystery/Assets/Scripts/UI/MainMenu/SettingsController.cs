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

    private float savedMusic;
    private float savedSFX;
    private int savedResolution;
    private bool savedFullscreen;

    private void OnEnable()
    {
        SetupSliders();
        LoadSavedSettings();
        AddListeners();
        UpdateApplyButtonState();
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

    private void LoadSavedSettings()
    {
        savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        savedSFX = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        savedResolution = PlayerPrefs.GetInt("ResolutionIndex", 0);
        savedFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
        resolutionDropdown.value = savedResolution;
        fullscreenText.text = savedFullscreen ? "ON" : "OFF";

        ApplyAudio();
        ApplyResolution();
        Screen.fullScreen = savedFullscreen;
    }

    private void AddListeners()
    {
        fullscreenButton.onClick.RemoveAllListeners();
        fullscreenButton.onClick.AddListener(ToggleFullscreen);

        musicSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);

        sfxSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);

        resolutionDropdown.onValueChanged.RemoveAllListeners();
        resolutionDropdown.onValueChanged.AddListener((_) => OnSettingChanged());

        applyButton.onClick.RemoveAllListeners();
        applyButton.onClick.AddListener(ApplySettings);
    }

    private void OnMusicSliderChanged(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;

        OnSettingChanged();
    }

    private void OnSFXSliderChanged(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;

        OnSettingChanged();
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
    }

    private void ApplyAudio()
    {
        if (musicSource != null)
            musicSource.volume = musicSlider.value;

        if (sfxSource != null)
            sfxSource.volume = sfxSlider.value;
    }

    private void UpdateApplyButtonState()
    {
        applyButton.interactable = settingsChanged;
    }

    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();

        savedMusic = musicSlider.value;
        savedSFX = sfxSlider.value;
        savedResolution = resolutionDropdown.value;
        savedFullscreen = Screen.fullScreen;

        ApplyResolution();

        settingsChanged = false;
        UpdateApplyButtonState();

        Debug.Log("Settings applied perfectly!");
    }

    private void ApplyResolution()
    {
        if (resolutionDropdown.options.Count == 0) return;
        if (resolutionDropdown.value < 0 || resolutionDropdown.value >= Screen.resolutions.Length) return;

        Resolution res = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void ReturnToMenu()
    {
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