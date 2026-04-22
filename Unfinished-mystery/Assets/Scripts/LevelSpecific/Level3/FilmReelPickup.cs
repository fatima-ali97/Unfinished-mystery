using UnityEngine;

public class FilmReelPickup : MonoBehaviour
{
    public GameObject interactPrompt;
    private bool playerInRange = false;
    private PlayerInventory playerInventory;

    private void Start()
    {
        if (interactPrompt != null)
        {
            interactPrompt.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Interact Prompt is NOT assigned!");
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed while near reel");

            if (playerInventory != null)
            {
                playerInventory.hasFilmReel = true;
                Debug.Log("Film reel picked up!");
            }
            else
            {
                Debug.LogWarning("PlayerInventory not found on player!");
            }

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered reel trigger");
            playerInRange = true;
            playerInventory = other.GetComponent<PlayerInventory>();

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited reel trigger");
            playerInRange = false;

            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }
    }
}