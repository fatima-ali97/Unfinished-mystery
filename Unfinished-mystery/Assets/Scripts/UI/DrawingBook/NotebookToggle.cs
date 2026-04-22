using UnityEngine;

public class NotebookToggle : MonoBehaviour
{
    public GameObject notebookPanel;

    public void ToggleNotebook()
    {
        bool isActive = !notebookPanel.activeSelf;
        notebookPanel.SetActive(isActive);

        if (isActive)
        {
            // When the book is OPEN
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // When the book is CLOSED (Change this based on your player controller)
            Cursor.visible = false; // Set to 'true' if you want a permanent cursor
            Cursor.lockState = CursorLockMode.Locked; // Locks mouse for 3D camera control
        }
    }
}