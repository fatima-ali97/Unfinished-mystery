using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoopingLevelTimer : MonoBehaviour
{
    public float loopDuration = 300f;
    public int maxLoops = 10;

    public TMP_Text timerText;
    public CanvasGroup redPulseOverlay;

    public GameObject loopEndedPanel;
    public CanvasGroup loopEndedCanvasGroup;
    public TMP_Text loopEndedTitle;
    public TMP_Text loopEndedMessage;

    public GameObject gameOverPanel;
    public Button exitButton;

    public float pulseMaxAlpha = 0.3f;
    public float pulseFadeInDuration = 0.35f;
    public float pulseFadeOutDuration = 0.45f;
    public float pulsePauseDuration = 0.35f;

    public float loopFadeIn = 1.5f;
    public float loopStay = 4f;
    public float loopFadeOut = 1.5f;

    public float shakeStrength = 5f;

    float timeLeft;
    int currentLoop = 0;

    bool hasShaken = false;
    bool isBusy = false;

    Coroutine pulseRoutine;
    Vector3 originalPos;

    void Start()
    {
        timeLeft = loopDuration;
        originalPos = timerText.transform.localPosition;

        if (redPulseOverlay != null)
            redPulseOverlay.alpha = 0f;

        if (loopEndedCanvasGroup != null)
            loopEndedCanvasGroup.alpha = 0f;

        if (loopEndedPanel != null)
            loopEndedPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        if (isBusy)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft < 0f)
            timeLeft = 0f;

        UpdateTimerUI();
        HandleEffects();

        if (timeLeft <= 0f)
        {
            currentLoop++;

            if (currentLoop >= maxLoops)
                StartCoroutine(ShowGameOver());
            else
                StartCoroutine(ShowLoopEnded());
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = minutes + ":" + seconds.ToString("00");
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

        if (timeLeft <= 10f && timeLeft > 0f)
        {
            if (pulseRoutine == null)
                pulseRoutine = StartCoroutine(PulseWarning());
        }
        else
        {
            StopPulse();
        }
    }

    IEnumerator ShakeOnce()
    {
        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float x = Random.Range(-shakeStrength, shakeStrength);
            float y = Random.Range(-shakeStrength, shakeStrength);
            timerText.transform.localPosition = originalPos + new Vector3(x, y, 0f);
            yield return null;
        }

        timerText.transform.localPosition = originalPos;
    }

    IEnumerator PulseWarning()
    {
        redPulseOverlay.alpha = 0f;

        while (true)
        {
            yield return StartCoroutine(FadeOverlay(0f, pulseMaxAlpha, pulseFadeInDuration));
            yield return StartCoroutine(FadeOverlay(pulseMaxAlpha, 0f, pulseFadeOutDuration));
            yield return new WaitForSeconds(pulsePauseDuration);
        }
    }

    IEnumerator FadeOverlay(float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            redPulseOverlay.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }

        redPulseOverlay.alpha = to;
    }

    void StopPulse()
    {
        if (pulseRoutine != null)
        {
            StopCoroutine(pulseRoutine);
            pulseRoutine = null;
        }

        if (redPulseOverlay != null)
            redPulseOverlay.alpha = 0f;
    }

    IEnumerator ShowLoopEnded()
    {
        isBusy = true;
        StopPulse();

        if (loopEndedPanel != null)
            loopEndedPanel.SetActive(true);

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

        if (loopEndedPanel != null)
            loopEndedPanel.SetActive(false);

        ResetLoop();
        isBusy = false;
    }

    IEnumerator ShowGameOver()
    {
        isBusy = true;
        StopPulse();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        CanvasGroup cg = gameOverPanel.GetComponent<CanvasGroup>();

        if (cg != null)
        {
            cg.alpha = 0f;
            float t = 0f;

            while (t < 2f)
            {
                t += Time.deltaTime;
                cg.alpha = Mathf.Lerp(0f, 1f, t / 2f);
                yield return null;
            }

            cg.alpha = 1f;
        }
    }

    void ResetLoop()
    {
        timeLeft = loopDuration;
        hasShaken = false;
    }

    void ExitGame()
    {
        SceneManager.LoadScene("LevelsBook");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}