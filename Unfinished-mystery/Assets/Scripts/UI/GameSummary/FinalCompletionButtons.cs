using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCompletionButtons : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public string playAgainSceneName = "LevelSummary";

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(playAgainSceneName);
    }
}