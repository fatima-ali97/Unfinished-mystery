using UnityEngine;

[CreateAssetMenu(fileName = "ClaudeConfig", menuName = "LLM/Claude Config")]
public class ClaudeConfig : ScriptableObject
{
    public string apiKey = "PASTE_YOUR_CLAUDE_API_KEY_HERE";
    public string apiUrl = "https://api.anthropic.com/v1/messages";
    public string anthropicVersion = "2023-06-01";
    public string model = "claude-3-5-haiku-latest";

    [Range(20, 300)]
    public int maxTokens = 120;
}