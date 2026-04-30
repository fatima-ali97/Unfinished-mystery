using UnityEngine;
using UnityEngine.Video;
using InventoryFramework;

public class FilmProjectorUse : MonoBehaviour
{
    public GameObject interactPrompt;

    [Header("Inventory")]
    public Item requiredReel;
    public Hotbar hotbar;

    [Header("Video")]
    public VideoPlayer videoPlayer;

    private bool playerInRange = false;
    private bool alreadyPlayed = false;

    private void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        if (videoPlayer != null)
            videoPlayer.Stop();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TryUseProjector();
        }
    }

    private void TryUseProjector()
    {
        if (alreadyPlayed)
        {
            Debug.Log("This reel has already been played.");
            return;
        }

        if (requiredReel == null)
        {
            Debug.LogError("Required Reel is not assigned!");
            return;
        }

        if (hotbar == null)
        {
            Debug.LogError("Hotbar is not assigned!");
            return;
        }

        bool removed = RemoveItem(requiredReel);

        if (removed)
        {
            Debug.Log("Reel inserted. Playing video.");

            alreadyPlayed = true;

            if (interactPrompt != null)
                interactPrompt.SetActive(false);

            if (videoPlayer != null)
                videoPlayer.Play();
            else
                Debug.LogError("VideoPlayer is not assigned!");
        }
        else
        {
            Debug.Log("You need the film reel first.");
        }
    }

    private bool RemoveItem(Item item)
    {
        foreach (var slot in hotbar.slots)
        {
            if (!slot.IsEmpty && slot.item == item)
            {
                slot.count--;

                if (slot.count <= 0)
                {
                    slot.item = null;
                    slot.count = 0;
                }

                FindAnyObjectByType<HotbarUI>()?.RefreshUI();
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!alreadyPlayed && interactPrompt != null)
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