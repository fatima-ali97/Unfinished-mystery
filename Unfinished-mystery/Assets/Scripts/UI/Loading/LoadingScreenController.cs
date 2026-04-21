using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreenController : MonoBehaviour
{
    [Header("Door")]
    public Image doorImage;
    public Sprite closedDoorSprite;
    public Sprite openDoorSprite;

    [Header("Audio")]
    public AudioSource backgroundMusicAudio;
    public AudioSource doorOpenAudio;

    [Header("UI")]
    public TextMeshProUGUI loadingText;
    public RectTransform barFill;
    public RectTransform barOutline;

    [Header("Scene")]
    public string nextSceneName = "Level1Scene";

    [Header("Timing")]
    public float minimumLoadingTime = 2f;
    public float openDoorDelay = 1f;

    private AsyncOperation asyncLoad;
    private float barHeight;
    private float maxBarWidth;

    void Start()
    {
        if (doorImage == null || closedDoorSprite == null || openDoorSprite == null || loadingText == null || barFill == null || barOutline == null)
        {
            Debug.LogError("LoadingScreenController: Missing references!");
            return;
        }

        // نخلي الشريط يتمدد من اليسار
        barFill.anchorMin = new Vector2(0f, 0.5f);
        barFill.anchorMax = new Vector2(0f, 0.5f);
        barFill.pivot = new Vector2(0f, 0.5f);
        barFill.anchoredPosition = new Vector2(0f, 0f);

        barHeight = barFill.sizeDelta.y;
        maxBarWidth = barOutline.rect.width;

        SetBarProgress(0f);

        // نشغل موسيقى الخلفية من البداية إذا موجودة
        if (backgroundMusicAudio != null && !backgroundMusicAudio.isPlaying)
        {
            backgroundMusicAudio.Play();
        }

        StartCoroutine(LoadSceneRoutine());
    }

    IEnumerator LoadSceneRoutine()
    {
        doorImage.sprite = closedDoorSprite;

        float timer = 0f;
        asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        if (asyncLoad == null)
        {
            Debug.LogError("Failed to load scene: " + nextSceneName);
            yield break;
        }

        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f || timer < minimumLoadingTime)
        {
            timer += Time.deltaTime;

            float sceneProgress = asyncLoad.progress / 0.9f;
            float timeProgress = timer / minimumLoadingTime;
            float progress = Mathf.Min(sceneProgress, timeProgress);
            progress = Mathf.Clamp01(progress);

            int percentage = Mathf.RoundToInt(progress * 100f);
            loadingText.text = "LOADING... " + percentage + "%";

            SetBarProgress(progress);

            yield return null;
        }

        loadingText.text = "LOADING... 100%";
        SetBarProgress(1f);

        // صوت الباب يشتغل فقط وقت الفتح
        if (doorOpenAudio != null)
        {
            doorOpenAudio.Play();
        }

        doorImage.sprite = openDoorSprite;

        yield return new WaitForSeconds(openDoorDelay);

        asyncLoad.allowSceneActivation = true;
    }

    void SetBarProgress(float progress)
    {
        float newWidth = maxBarWidth * progress;
        barFill.sizeDelta = new Vector2(newWidth, barHeight);
    }
}