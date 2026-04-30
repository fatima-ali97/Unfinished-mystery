using UnityEngine;

public class FilmProjectorInteraction : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject screenMessage;

    private bool playerInRange = false;
    private PlayerInventory playerInventory;

    private void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        if (screenMessage != null)
            screenMessage.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory == null)
            {
                Debug.LogWarning("PlayerInventory not found!");
                return;
            }

            if (playerInventory.hasFilmReel)
            {
                Debug.Log("Film reel inserted into projector!");

                if (screenMessage != null)
                    screenMessage.SetActive(true);
            }
            else
            {
                Debug.Log("You need a film reel first!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();

            if (interactPrompt != null)
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