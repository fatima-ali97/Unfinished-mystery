public static class PromptBuilder
{
    public static string BuildMemoryNotePrompt(
        string levelName,
        int loopNumber,
        string realClue,
        string mood,
        string playerActions = ""
    )
    {
        return
            "You are writing a memory journal entry for a horror escape room game.\n\n" +

            "RULES:\n" +
            "- Write ONLY 1–3 short sentences.\n" +
            "- Keep it mysterious and fragmented like broken memory.\n" +
            "- Do NOT reveal full solutions or final answers.\n" +
            "- Do NOT create new puzzle codes or numbers.\n" +
            "- Do NOT mention AI, Claude, or system.\n" +
            "- Make it feel emotional, unclear, and slightly unreliable.\n\n" +

            "LEVEL CONTEXT:\n" +
            "Level: " + levelName + "\n" +
            "Loop: " + loopNumber + "\n" +
            "Mood: " + mood + "\n\n" +

            "WHAT PLAYER DID:\n" +
            playerActions + "\n\n" +

            "HIDDEN TRUTH INFLUENCE (DO NOT EXPLAIN DIRECTLY):\n" +
            realClue + "\n\n" +

            "Now write the memory journal entry:";
    }
}