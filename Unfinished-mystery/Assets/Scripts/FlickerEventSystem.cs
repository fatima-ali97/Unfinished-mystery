using System.Collections;
using UnityEngine;

public class FlickerEventSystem : MonoBehaviour
{
    [Header("Light Settings")]
    public Light sceneLight;

    [Header("Flicker Settings")]
    public float minTimeBetweenFlickers = 3f;
    public float maxTimeBetweenFlickers = 10f;

    [Range(0.05f, 0.5f)]
    public float flickerDuration = 0.1f;

    [Header("Loop Intensity (optional)")]
    public int currentLoop = 1;

    private float originalIntensity;

    private void Start()
    {
        if (sceneLight != null)
            originalIntensity = sceneLight.intensity;

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenFlickers, maxTimeBetweenFlickers);

            yield return new WaitForSeconds(waitTime);

            TriggerFlicker();
        }
    }

    public void TriggerFlicker()
    {
        if (sceneLight == null) return;

        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        // Intensity gets stronger with loop (simple horror scaling)
        float intensityMultiplier = 1f + (currentLoop * 0.1f);

        sceneLight.intensity = originalIntensity * 0.2f * intensityMultiplier;
        yield return new WaitForSeconds(flickerDuration);

        sceneLight.intensity = originalIntensity;
    }

    // Call this from your Loop system
    public void SetLoop(int loop)
    {
        currentLoop = loop;
    }
}