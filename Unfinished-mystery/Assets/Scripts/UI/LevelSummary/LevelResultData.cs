using UnityEngine;

public static class LevelResultData
{
    public static string title = "LEVEL COMPLETE";
    public static string levelName = "Level 1";
    public static string characterName = "Kyryll Flins";
    public static string role = "Mathematics Professor";
    public static int loopsUsed = 3;
    public static string resultMessage = "The truth has been uncovered.";
    public static Sprite portrait = null;
    public static string nextSceneName = "";
    public static string replaySceneName = "";

    public static void SetResult(
        string newTitle,
        string newLevelName,
        string newCharacterName,
        string newRole,
        int newLoopsUsed,
        string newResultMessage,
        Sprite newPortrait,
        string newNextSceneName,
        string newReplaySceneName)
    {
        title = newTitle;
        levelName = newLevelName;
        characterName = newCharacterName;
        role = newRole;
        loopsUsed = newLoopsUsed;
        resultMessage = newResultMessage;
        portrait = newPortrait;
        nextSceneName = newNextSceneName;
        replaySceneName = newReplaySceneName;
    }

    public static void ResetData()
    {
        title = "LEVEL COMPLETE";
        levelName = "Level 1";
        characterName = "Kyryll Flins";
        role = "Mathematics Professor";
        loopsUsed = 3;
        resultMessage = "The truth has been uncovered.";
        portrait = null;
        nextSceneName = "";
        replaySceneName = "";
    }
}