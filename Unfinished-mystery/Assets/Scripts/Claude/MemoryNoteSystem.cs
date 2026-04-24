using UnityEngine;

public class MemoryNoteSystem : MonoBehaviour
{
    [Header("Level Info")]
    public string levelName;
    public string mood;
    public string realClue;

    [Header("Loop")]
    public int currentLoop = 1;

    private string playerActions = "";

    private void Start()
    {
        GenerateNote();
    }

    public void AddPlayerAction(string action)
    {
        playerActions += "- " + action + "\n";
    }

    public void IncreaseLoop()
    {
        currentLoop++;
        playerActions = "";
        GenerateNote();
    }

    public void GenerateNote()
    {
        if (ClaudeManager.Instance == null)
        {
            JournalUI.Instance.AddNote("Memory broken...");
            return;
        }

        string prompt = PromptBuilder.BuildMemoryNotePrompt(
            levelName,
            currentLoop,
            realClue,
            mood,
            playerActions
        );

        ClaudeManager.Instance.GenerateNote(
            prompt,
            (note) => JournalUI.Instance.AddNote(note),
            (err) => JournalUI.Instance.AddNote("The memory slips away... but something still feels wrong."));
    }
}