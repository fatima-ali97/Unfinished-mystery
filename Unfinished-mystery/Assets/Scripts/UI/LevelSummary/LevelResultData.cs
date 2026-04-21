using UnityEngine;

public static class LevelResultData
{
    public static string title;
    public static string levelName;
    public static string characterName;
    public static string role;
    public static int loopsUsed;
    public static string resultMessage;
    public static Sprite portrait;
    public static string nextSceneName;
    public static string replaySceneName;

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
        levelName = "";
        characterName = "";
        role = "";
        loopsUsed = 0;
        resultMessage = "";
        portrait = null;
        nextSceneName = "";
        replaySceneName = "";
    }
}