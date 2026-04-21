using UnityEngine;
using TMPro;

public class SimpleInteractable : MonoBehaviour
{
    [TextArea] public string message;
    public GameObject interactPrompt;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            messagePanel.SetActive(true);
            messageText.text = message;
        }

        if (messagePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            messagePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            interactPrompt.SetActive(false);
        }
    }
}