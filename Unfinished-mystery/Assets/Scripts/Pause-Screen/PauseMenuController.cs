using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    [Header("Pause UI")]
    public GameObject pauseCanvas;

    [Header("Icon Images")]
    public Image soundIconImage;
    public Image musicIconImage;

    [Header("Sound Icons")]
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    [Header("Music Icons")]
    public Sprite musicOnIcon;
    public Sprite musicOffIcon;

    [Header("UI Audio")]
    public AudioSource uiAudioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;

    [Header("Optional Level Music")]
    public AudioSource backgroundMusicSource;

    [Header("Optional Levels Scene")]
    public string levelsSceneName = "";

    private bool isPaused = false;
    private bool soundMuted = false;
    private bool musicMuted = false;

    private void Start()
    {
        Time.timeScale = 1f;

        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        UpdateAudioIcons();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    
    public void PauseGame()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true);

        isPaused = true;
        Time.timeScale = 0f;

        if (backgroundMusicSource != null && !musicMuted)
            backgroundMusicSource.Pause();
    }

    public void ResumeGame()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        isPaused = false;
        Time.timeScale = 1f;

        if (backgroundMusicSource != null && !musicMuted)
            backgroundMusicSource.UnPause();
    }

   
    public void RestartLevel()
    {
        StartCoroutine(RestartLevelAfterClick());
    }

    private IEnumerator RestartLevelAfterClick()
    {
        // wait using REAL TIME (works even when paused)
        yield return new WaitForSecondsRealtime(0.15f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

   
    public void GoToLevels()
    {
        if (!string.IsNullOrWhiteSpace(levelsSceneName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(levelsSceneName);
        }
        else
        {
            Debug.Log("Levels scene not assigned yet.");
        }
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Quit Game");
    }

    
    public void ToggleSound()
    {
        soundMuted = !soundMuted;
        UpdateAudioIcons();
    }

    public void ToggleMusic()
    {
        musicMuted = !musicMuted;

        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.mute = musicMuted;
        }

        UpdateAudioIcons();
    }

   
    public void PlayHoverSound()
    {
        if (!soundMuted && uiAudioSource != null && hoverClip != null)
        {
            uiAudioSource.PlayOneShot(hoverClip);
        }
    }

    public void PlayClickSound()
    {
        if (!soundMuted && uiAudioSource != null && clickClip != null)
        {
            uiAudioSource.PlayOneShot(clickClip);
        }
    }

   
    private void UpdateAudioIcons()
    {
        if (soundIconImage != null)
            soundIconImage.sprite = soundMuted ? soundOffIcon : soundOnIcon;

        if (musicIconImage != null)
            musicIconImage.sprite = musicMuted ? musicOffIcon : musicOnIcon;
    }
}