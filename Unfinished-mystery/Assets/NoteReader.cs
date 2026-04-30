using UnityEngine;
using TMPro;

public class NoteReader : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject notePanel;
    public TMP_Text noteText;

    [TextArea(3, 8)]
    public string message;

    private bool playerInRange = false;
    private bool noteOpen = false;

    private void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        if (notePanel != null)
            notePanel.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!noteOpen)
            {
                OpenNote();
            }
            else
            {
                CloseNote();
            }
        }

        if (noteOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseNote();
        }
    }

    private void OpenNote()
    {
        noteOpen = true;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        if (notePanel != null)
            notePanel.SetActive(true);

        if (noteText != null)
            noteText.text = message;

        Time.timeScale = 0f;
    }

    private void CloseNote()
    {
        noteOpen = false;

        if (notePanel != null)
            notePanel.SetActive(false);

        if (playerInRange && interactPrompt != null)
            interactPrompt.SetActive(true);

        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!noteOpen && interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }
    }
}