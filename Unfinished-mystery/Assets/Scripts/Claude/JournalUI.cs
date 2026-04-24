using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalUI : MonoBehaviour
{
    public static JournalUI Instance;

    [Header("UI")]
    public GameObject journalPanel;
    public TMP_Text journalText;

    private List<string> notes = new List<string>();
    private bool isOpen = false;

    private void Awake()
    {
        Instance = this;

        if (journalPanel != null)
            journalPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleJournal();
        }
    }

    public void AddNote(string note)
    {
        notes.Add(note);
        RefreshJournal();
    }

    private void ToggleJournal()
    {
        isOpen = !isOpen;

        if (journalPanel != null)
            journalPanel.SetActive(isOpen);

        RefreshJournal();
    }

    private void RefreshJournal()
    {
        if (journalText == null)
            return;

        journalText.text = "";

        for (int i = 0; i < notes.Count; i++)
        {
            journalText.text += "Memory " + (i + 1) + ":\n";
            journalText.text += notes[i] + "\n\n";
        }
    }
}