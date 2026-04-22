using UnityEngine;

public class TVInteraction : MonoBehaviour
{
    public GameObject tvMessage;

    private void Awake()
    {
        if (tvMessage != null)
            tvMessage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED TV");
            if (tvMessage != null)
                tvMessage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER EXITED TV");
            if (tvMessage != null)
                tvMessage.SetActive(false);
        }
    }
}