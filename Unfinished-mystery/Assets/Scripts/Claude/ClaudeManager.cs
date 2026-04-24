using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ClaudeManager : MonoBehaviour
{
    public static ClaudeManager Instance;

    [SerializeField] private ClaudeConfig config;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GenerateNote(string prompt, Action<string> onSuccess, Action<string> onError)
    {
        StartCoroutine(SendRequest(prompt, onSuccess, onError));
    }

    private IEnumerator SendRequest(string prompt, Action<string> onSuccess, Action<string> onError)
    {
        if (config == null)
        {
            onError?.Invoke("ClaudeConfig is missing.");
            yield break;
        }

        if (string.IsNullOrEmpty(config.apiKey))
        {
            onError?.Invoke("Claude API key is missing.");
            yield break;
        }

        string jsonBody =
            "{"
            + "\"model\":\"" + EscapeJson(config.model) + "\","
            + "\"max_tokens\":" + config.maxTokens + ","
            + "\"messages\":[{\"role\":\"user\",\"content\":\"" + EscapeJson(prompt) + "\"}]"
            + "}";

        UnityWebRequest request = new UnityWebRequest(config.apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-api-key", config.apiKey);
        request.SetRequestHeader("anthropic-version", config.anthropicVersion);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error + "\n" + request.downloadHandler.text);
            yield break;
        }

        string note = ExtractClaudeText(request.downloadHandler.text);

        if (string.IsNullOrEmpty(note))
        {
            onError?.Invoke("Could not read Claude response.");
            yield break;
        }

        onSuccess?.Invoke(note.Trim());
    }

    private string ExtractClaudeText(string json)
    {
        string marker = "\"text\":\"";
        int start = json.IndexOf(marker, StringComparison.Ordinal);

        if (start == -1)
            return "";

        start += marker.Length;

        int end = json.IndexOf("\"", start, StringComparison.Ordinal);

        while (end > 0 && json[end - 1] == '\\')
        {
            end = json.IndexOf("\"", end + 1, StringComparison.Ordinal);
        }

        if (end == -1)
            return "";

        string text = json.Substring(start, end - start);
        return UnescapeJson(text);
    }

    private string EscapeJson(string text)
    {
        if (string.IsNullOrEmpty(text))
            return "";

        return text
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "");
    }

    private string UnescapeJson(string text)
    {
        return text
            .Replace("\\n", "\n")
            .Replace("\\\"", "\"")
            .Replace("\\\\", "\\");
    }
}