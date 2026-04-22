using UnityEngine;
using System.Collections;

public class TVInteraction : MonoBehaviour
{
    public GameObject tvStatic;   // TV static (glitch effect)
    public GameObject tvMessage;  // Text message displayed after static
    public AudioSource staticSound; // Audio source for static sound

    private bool playerNear = false; // Tracks if player is inside trigger area
    private bool isPlaying = false;  // Prevents replaying sequence while active

    void Start()
    {
        // Ensure both static and message are hidden at the start
        if (tvStatic != null) tvStatic.SetActive(false);
        if (tvMessage != null) tvMessage.SetActive(false);
    }

    void Update()
    {
        // If player is near, presses E, and sequence is not already playing
        if (playerNear && Input.GetKeyDown(KeyCode.E) && !isPlaying)
        {
            StartCoroutine(TVSequence());
        }
    }

    IEnumerator TVSequence()
    {
        isPlaying = true;

        // Show static effect
        if (tvStatic != null)
            tvStatic.SetActive(true);

        // Play sound safely
        if (staticSound != null)
            staticSound.Play();

        // Wait for 1 second (duration of static)
        yield return new WaitForSeconds(1f);

        // Hide static
        if (tvStatic != null)
            tvStatic.SetActive(false);

        // Stop sound safely
        if (staticSound != null)
            staticSound.Stop();

        // Show the message
        if (tvMessage != null)
            tvMessage.SetActive(true);

        isPlaying = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Detect when player enters trigger area
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        // Reset everything when player leaves the area
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (tvStatic != null)
                tvStatic.SetActive(false);

            if (tvMessage != null)
                tvMessage.SetActive(false);

            if (staticSound != null)
                staticSound.Stop();
        }
    }
}