public static class PromptBuilder
{
    public static string BuildMemoryNotePrompt(string levelName, int loopNumber, string realClue, string mood)
    {
        return
            "Write one short mysterious memory journal note for a horror escape room game.\n" +
            "Rules:\n" +
            "- 1 to 3 sentences only.\n" +
            "- Do not reveal the full solution.\n" +
            "- Do not invent puzzle codes.\n" +
            "- Do not mention AI.\n" +
            "- Make it feel like a broken memory.\n\n" +
            "Level: " + levelName + "\n" +
            "Loop number: " + loopNumber + "\n" +
            "Mood: " + mood + "\n" +
            "Hidden real clue inspiration: " + realClue;
    }
}