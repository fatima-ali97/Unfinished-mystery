using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSummaryButtons : MonoBehaviour
{
    public string nextSceneName = "";
    public string replaySceneName = "";

    public void ContinueGame()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("No next scene set.");
        }
    }

    public void ReplayLevel()
    {
        if (!string.IsNullOrEmpty(replaySceneName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(replaySceneName);
        }
        else
        {
            Debug.Log("No replay scene set.");
        }
    }
}