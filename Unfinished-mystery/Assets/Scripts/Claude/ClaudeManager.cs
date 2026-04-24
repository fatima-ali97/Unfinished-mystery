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
        string jsonBody =
            "{"
            + "\"model\":\"" + config.model + "\","
            + "\"max_tokens\":" + config.maxTokens + ","
            + "\"messages\":[{\"role\":\"user\",\"content\":\"" + Escape(prompt) + "\"}]"
            + "}";

        UnityWebRequest request = new UnityWebRequest(config.apiUrl, "POST");
        byte[] body = Encoding.UTF8.GetBytes(jsonBody);

        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-api-key", config.apiKey);
        request.SetRequestHeader("anthropic-version", config.anthropicVersion);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error);
            yield break;
        }

        string response = request.downloadHandler.text;

        // SIMPLE PARSE
        string marker = "\"text\":\"";
        int start = response.IndexOf(marker);

        if (start == -1)
        {
            onError?.Invoke("Parse error");
            yield break;
        }

        start += marker.Length;
        int end = response.IndexOf("\"", start);

        string note = response.Substring(start, end - start);
        note = note.Replace("\\n", "\n");

        onSuccess?.Invoke(note);
    }

    private string Escape(string text)
    {
        return text.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}