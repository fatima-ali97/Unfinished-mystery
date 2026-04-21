using UnityEngine;

public class TVInteraction : MonoBehaviour
{
    public GameObject tvMessage;
    private bool playerNear = false;
    private bool messageOpen = false;

    void Start()
    {
        if (tvMessage != null)
            tvMessage.SetActive(false);
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            messageOpen = !messageOpen;

            if (tvMessage != null)
                tvMessage.SetActive(messageOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            messageOpen = false;

            if (tvMessage != null)
                tvMessage.SetActive(false);
        }
    }
}