using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSummaryButtons : MonoBehaviour
{
    public void ContinueGame()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(LevelResultData.nextSceneName))
        {
            SceneManager.LoadScene(LevelResultData.nextSceneName);
        }
        else
        {
            Debug.LogWarning("No next scene set.");
        }
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(LevelResultData.replaySceneName))
        {
            SceneManager.LoadScene(LevelResultData.replaySceneName);
        }
        else
        {
            Debug.LogWarning("No replay scene set.");
        }
    }
}