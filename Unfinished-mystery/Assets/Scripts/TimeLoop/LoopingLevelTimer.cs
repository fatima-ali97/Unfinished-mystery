using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoopingLevelTimer : MonoBehaviour
{
    public float loopDuration = 300f;
    public int maxLoops = 10;

    public TextMeshProUGUI timerText;
    public CanvasGroup redPulseOverlay;

    public CanvasGroup loopEndedCanvasGroup;
    public TMP_Text loopEndedTitle;
    public TMP_Text loopEndedMessage;

    public GameObject gameOverPanel;
    public Button exitButton;

    public float shakeAmount = 5f;

    public float pulseMaxAlpha = 0.25f;
    public float pulseSpeed = 0.6f;

    public float loopFadeIn = 2f;
    public float loopStay = 6f;
    public float loopFadeOut = 2f;

    float timeLeft;
    int currentLoop = 0;

    Vector3 originalTimerPos;
    bool hasShaken = false;
    bool isPaused = false;

    Coroutine pulseRoutine;

    void Start()
    {
        timeLeft = loopDuration;
        originalTimerPos = timerText.rectTransform.localPosition;

        redPulseOverlay.alpha = 0f;
        loopEndedCanvasGroup.alpha = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        exitButton.onClick.AddListener(ExitGame);

        UpdateTimerUI();
    }

    void Update()
    {
        if (isPaused)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            currentLoop++;
            hasShaken = false;

            if (currentLoop >= maxLoops)
            {
                ShowGameOver();
                return;
            }

            StartCoroutine(LoopSequence());
            return;
        }

        UpdateTimerUI();
        HandleEffects();
    }

    void UpdateTimerUI()
    {
        int m = Mathf.FloorToInt(timeLeft / 60f);
        int s = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = $"{m}:{s:00}";
    }

    void HandleEffects()
    {
        if (timeLeft <= 60f)
        {
            timerText.color = Color.red;

            if (!hasShaken)
            {
                StartCoroutine(ShakeOnce());
                hasShaken = true;
            }
        }
        else
        {
            timerText.color = Color.white;
        }

        if (timeLeft <= 10f)
        {
            if (pulseRoutine == null)
                pulseRoutine = StartCoroutine(Pulse());
        }
        else
        {
            StopPulse();
        }
    }

    IEnumerator ShakeOnce()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            Vector2 offset = Random.insideUnitCircle * shakeAmount;
            timerText.rectTransform.localPosition = originalTimerPos + new Vector3(offset.x, offset.y, 0);
            yield return null;
        }

        timerText.rectTransform.localPosition = originalTimerPos;
    }

    IEnumerator Pulse()
    {
        while (true)
        {
            float a = (Mathf.Sin(Time.time * pulseSpeed * Mathf.PI * 2f) + 1f) * 0.5f;
            redPulseOverlay.alpha = Mathf.Lerp(0f, pulseMaxAlpha, a);
            yield return null;
        }
    }

    void StopPulse()
    {
        if (pulseRoutine != null)
        {
            StopCoroutine(pulseRoutine);
            pulseRoutine = null;
        }

        redPulseOverlay.alpha = 0f;
    }

    IEnumerator LoopSequence()
    {
        isPaused = true;
        StopPulse();

        timerText.rectTransform.localPosition = originalTimerPos;

        float t = 0f;

        while (t < loopFadeIn)
        {
            t += Time.deltaTime;
            loopEndedCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t / loopFadeIn);
            yield return null;
        }

        loopEndedCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(loopStay);

        t = 0f;

        while (t < loopFadeOut)
        {
            t += Time.deltaTime;
            loopEndedCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t / loopFadeOut);
            yield return null;
        }

        loopEndedCanvasGroup.alpha = 0f;

        timeLeft = loopDuration;
        isPaused = false;
    }

    void ShowGameOver()
    {
        StopPulse();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        enabled = false;
    }

    void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}