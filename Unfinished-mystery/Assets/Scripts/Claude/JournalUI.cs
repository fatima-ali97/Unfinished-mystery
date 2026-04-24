using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalUI : MonoBehaviour
{
    public static JournalUI Instance;

    public GameObject panel;
    public TMP_Text text;

    private List<string> notes = new List<string>();

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            panel.SetActive(!panel.activeSelf);

        Refresh();
    }

    public void AddNote(string note)
    {
        notes.Add(note);
    }

    private void Refresh()
    {
        text.text = "";

        for (int i = 0; i < notes.Count; i++)
        {
            text.text += "Memory " + (i + 1) + ":\n" + notes[i] + "\n\n";
        }
    }
}