using UnityEngine;
using TMPro;
using System.Collections;

public class TVInteraction : MonoBehaviour
{
    [Header("Message")]
    [TextArea]
    public string message = "She drew what I refused to write.";

    [Header("Objects")]
    public GameObject tvStatic;
    public GameObject tvMessage;
    public TMP_Text messageText;

    [Header("Audio")]
    public AudioSource staticSound;

    [Header("Timing")]
    public float staticDuration = 1f;
    public bool playOnlyOncePerLoop = true;

    private bool hasPlayed = false;
    private bool isPlaying = false;

    void Start()
    {
        if (tvStatic != null)
            tvStatic.SetActive(false);

        if (tvMessage != null)
            tvMessage.SetActive(false);

        if (messageText != null)
            messageText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isPlaying)
            return;

        if (playOnlyOncePerLoop && hasPlayed)
            return;

        StartCoroutine(TVSequence());
    }

    private IEnumerator TVSequence()
    {
        isPlaying = true;
        hasPlayed = true;

        if (tvMessage != null)
            tvMessage.SetActive(false);

        if (tvStatic != null)
            tvStatic.SetActive(true);

        if (staticSound != null)
            staticSound.Play();

        yield return new WaitForSeconds(staticDuration);

        if (tvStatic != null)
            tvStatic.SetActive(false);

        if (staticSound != null)
            staticSound.Stop();

        if (tvMessage != null)
            tvMessage.SetActive(true);

        if (messageText != null)
            messageText.text = message;

        isPlaying = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (tvStatic != null)
            tvStatic.SetActive(false);

        if (staticSound != null)
            staticSound.Stop();
    }
}