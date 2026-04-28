using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all main menu functionality:
/// - Scene navigation (Start Game)
/// - UI panels (Settings, Credits, Instructions)
/// - Application control (Quit)
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    [Tooltip("Panel for Main Menu")]
    public GameObject mainMenuPanel;

    [Tooltip("Panel for Settings menu")]
    public GameObject settingsPanel;

    [Tooltip("Panel for Credits screen")]
    public GameObject creditsPanel;

    [Tooltip("Panel for Instructions screen")]
    public GameObject instructionsPanel;

    void Start()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (instructionsPanel != null) instructionsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelsBook");
    }

    public void OpenSettings()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (instructionsPanel != null) instructionsPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
        else
            Debug.LogWarning("Settings Panel is not assigned.");
    }

    public void OpenCredits()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (instructionsPanel != null) instructionsPanel.SetActive(false);

        if (creditsPanel != null)
            creditsPanel.SetActive(true);
        else
            Debug.LogWarning("Credits Panel is not assigned.");
    }

    public void OpenInstructions()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);

        if (instructionsPanel != null)
            instructionsPanel.SetActive(true);
        else
            Debug.LogWarning("Instructions Panel is not assigned.");
    }

    public void ReturnToMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (instructionsPanel != null) instructionsPanel.SetActive(false);
    }

    public void ClosePanel(GameObject panel)
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}