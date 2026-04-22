using UnityEngine;

public class NotebookToggle : MonoBehaviour
{
    public GameObject notebookPanel;

    public void ToggleNotebook()
    {
        // This flips the state:
        bool isActive = notebookPanel.activeSelf;
        notebookPanel.SetActive(!isActive);

        // Optional locks/unlocks the players mouse cursor
        if (!isActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}