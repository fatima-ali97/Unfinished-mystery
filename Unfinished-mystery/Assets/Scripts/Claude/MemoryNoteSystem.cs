using UnityEngine;

public class MemoryNoteSystem : MonoBehaviour
{
    [Header("Level Settings")]
    public string levelName = "Projector Room";
    public string mood = "scary, confusing, mysterious";

    [Header("Loop Settings")]
    public int currentLoop = 1;

    [Header("Real Clue")]
    [TextArea]
    public string realClue = "The projector needs the film reel before the exit can open.";

    [Header("Fallback Note")]
    [TextArea]
    public string fallbackNote = "I remember the projector flickering... something important was missing.";

    private void Start()
    {
        GenerateMemoryNote();
    }

    public void IncreaseLoop()
    {
        currentLoop++;
        GenerateMemoryNote();
    }

    public void GenerateMemoryNote()
    {
        if (ClaudeManager.Instance == null)
        {
            Debug.LogWarning("ClaudeManager is missing from the scene.");
            JournalUI.Instance.AddNote(fallbackNote);
            return;
        }

        string prompt = PromptBuilder.BuildMemoryNotePrompt(levelName, currentLoop, realClue, mood);

        ClaudeManager.Instance.GenerateNote(prompt, OnNoteGenerated, OnNoteFailed);
    }

    private void OnNoteGenerated(string note)
    {
        JournalUI.Instance.AddNote(note);
    }

    private void OnNoteFailed(string error)
    {
        Debug.LogWarning("Claude failed: " + error);
        JournalUI.Instance.AddNote(fallbackNote);
    }
}