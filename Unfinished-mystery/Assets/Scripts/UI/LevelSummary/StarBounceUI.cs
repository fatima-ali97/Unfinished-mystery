using System.Collections;
using UnityEngine;

public class StarBounceUI : MonoBehaviour
{
    public float dropDistance = 80f;
    public float dropDuration = 0.25f;
    public float bounceScale = 1.2f;
    public float bounceDuration = 0.15f;

    private RectTransform rectTransform;
    private Vector2 finalPosition;
    private Vector3 finalScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        finalPosition = rectTransform.anchoredPosition;
        finalScale = Vector3.one;
    }

    public void ResetStar()
    {
        rectTransform.anchoredPosition = finalPosition + new Vector2(0f, dropDistance);
        rectTransform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public IEnumerator PlayAnimation()
    {
        gameObject.SetActive(true);

        Vector2 startPos = finalPosition + new Vector2(0f, dropDistance);
        Vector2 endPos = finalPosition;

        float time = 0f;

        while (time < dropDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / dropDuration);

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            rectTransform.localScale = Vector3.Lerp(Vector3.zero, finalScale, t);

            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
        rectTransform.localScale = finalScale;

        time = 0f;
        Vector3 bounceTarget = finalScale * bounceScale;

        while (time < bounceDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / bounceDuration);
            rectTransform.localScale = Vector3.Lerp(finalScale, bounceTarget, t);
            yield return null;
        }

        time = 0f;
        while (time < bounceDuration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / bounceDuration);
            rectTransform.localScale = Vector3.Lerp(bounceTarget, finalScale, t);
            yield return null;
        }

        rectTransform.localScale = finalScale;
    }
}