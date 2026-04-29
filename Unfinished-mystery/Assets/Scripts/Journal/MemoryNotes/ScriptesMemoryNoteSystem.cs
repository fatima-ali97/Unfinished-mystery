using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptedMemoryNoteSystem : MonoBehaviour
{
    public static ScriptedMemoryNoteSystem Instance;

    [Header("Level")]
    [Range(1, 5)] public int levelNumber = 1;

    [Header("Loop")]
    [Range(1, 10)] public int currentLoop = 1;

    [Header("UI")]
    public GameObject journalPanel;
    public TMP_Text journalText;

    private readonly List<string> savedNotes = new List<string>();
    private readonly HashSet<string> discoveredClues = new HashSet<string>();

    private void Awake()
    {
        Instance = this;

        if (journalPanel != null)
            journalPanel.SetActive(false);
    }

    private void Start()
    {
        GenerateMemoryNote();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && journalPanel != null)
            journalPanel.SetActive(!journalPanel.activeSelf);
    }

    public void NextLoop()
    {
        currentLoop = Mathf.Clamp(currentLoop + 1, 1, 10);
        GenerateMemoryNote();
    }

    public void AddClue(string clueName)
    {
        if (!discoveredClues.Contains(clueName))
        {
            discoveredClues.Add(clueName);
            GenerateMemoryNote();
        }
    }

    public void GenerateMemoryNote()
    {
        string note = BuildNote(levelNumber, currentLoop);
        savedNotes.Add(note);
        RefreshJournal();
    }

    private void RefreshJournal()
    {
        if (journalText == null) return;

        journalText.text = "";

        for (int i = 0; i < savedNotes.Count; i++)
        {
            journalText.text += "Memory " + (i + 1) + "\n";
            journalText.text += savedNotes[i] + "\n\n";
        }
    }

    private string BuildNote(int level, int loop)
    {
        string intro = GetRandom(Intros());
        string body = "";

        if (level == 1) body = Level1Professor(loop);
        else if (level == 2) body = Level2Detective(loop);
        else if (level == 3) body = Level3Cinema(loop);
        else if (level == 4) body = Level4Photographer(loop);
        else if (level == 5) body = Level5Doctor(loop);
        else body = "The memory refuses to form.";

        string ending = GetRandom(Endings(loop));

        return intro + " " + body + " " + ending;
    }

    private string[] Intros()
    {
        return new string[]
        {
            "A broken thought returns.",
            "The room whispers again.",
            "Something inside me remembers.",
            "The memory is unclear, but it refuses to disappear.",
            "A fragment surfaces from the last loop."
        };
    }

    private string[] Endings(int loop)
    {
        if (loop <= 3)
        {
            return new string[]
            {
                "I do not understand it yet.",
                "Maybe I left this for myself.",
                "The answer is still buried."
            };
        }

        if (loop <= 6)
        {
            return new string[]
            {
                "I should look closer next time.",
                "The pattern is starting to form.",
                "One object is leading to another."
            };
        }

        return new string[]
        {
            "There is not much time left.",
            "The truth is almost complete.",
            "This time, I must not ignore it."
        };
    }

    // LEVEL 1
    private string Level1Professor(int loop)
    {
        if (discoveredClues.Contains("fibonacci"))
            return "The symbol F(12) keeps pulling my attention back to the laptop.";

        if (loop <= 3)
            return GetRandom(new string[]
            {
                "The office looks organized, but the papers feel staged.",
                "Numbers are scattered everywhere, like someone wanted them to be seen."
            });

        if (loop <= 6)
            return GetRandom(new string[]
            {
                "The sequence is not decoration. It is part of the lock.",
                "The professor trusted patterns more than words."
            });

        return GetRandom(new string[]
        {
            "F(12) matters. The laptop is waiting for the result.",
            "The hidden answer is inside the Fibonacci clue."
        });
    }

    // LEVEL 2
    private string Level2Detective(int loop)
    {
        if (discoveredClues.Contains("drawing"))
            return "Isla drew what Lana could not write. The numbers in the picture matter.";

        if (discoveredClues.Contains("badge"))
            return "259 is not only a number. It belongs to someone in uniform.";

        if (discoveredClues.Contains("phone"))
            return "The phone carries the voice Lana tried to bury.";

        if (loop <= 3)
            return GetRandom(new string[]
            {
                "The apartment is not empty. It remembers Lana’s silence.",
                "Every object feels placed, like a confession she never finished."
            });

        if (loop <= 6)
            return GetRandom(new string[]
            {
                "The child’s drawing is more than a memory. Count what Isla left behind.",
                "The bookshelf hides the shape of the truth."
            });

        return GetRandom(new string[]
        {
            "259 points to the badge. The frame hides the number. The clock waits after the voice.",
            "After Isla speaks, the frozen time becomes the final code."
        });
    }

    // LEVEL 3
    private string Level3Cinema(int loop)
    {
        if (discoveredClues.Contains("reel"))
            return "The reels are fragments of one buried story. Their order matters.";

        if (loop <= 3)
            return GetRandom(new string[]
            {
                "The cinema is quiet, but the screen feels awake.",
                "The projector waits like it knows what happened."
            });

        if (loop <= 6)
            return GetRandom(new string[]
            {
                "The wrong reel only shows noise. The correct sequence will speak.",
                "The shadows on the screen are not random."
            });

        return GetRandom(new string[]
        {
            "Watch the reels in order. The missing girl’s truth is inside the scenes.",
            "The final reel completes what the others only hinted at."
        });
    }

    // LEVEL 4
    private string Level4Photographer(int loop)
    {
        if (discoveredClues.Contains("photos"))
            return "The photographs are evidence. The highlighted words are directions.";

        if (loop <= 3)
            return GetRandom(new string[]
            {
                "The archive looks complete, but something has been removed.",
                "The shelves are too neat. Someone cleaned the truth away."
            });

        if (loop <= 6)
            return GetRandom(new string[]
            {
                "The photos point toward books. The words are not random.",
                "The missing pages are louder than the pages left behind."
            });

        return GetRandom(new string[]
        {
            "Fifth, Second, Eighth. Turn the words into numbers.",
            "528 opens what was erased."
        });
    }

    // LEVEL 5
    private string Level5Doctor(int loop)
    {
        if (discoveredClues.Contains("ledger"))
            return "The ledger, the samples, and the cabinet all point to the same lie.";

        if (discoveredClues.Contains("isla"))
            return "Isla’s name is not just a patient record. It is the key to the truth.";

        if (loop <= 3)
            return GetRandom(new string[]
            {
                "The lab is too clean. Something terrible was washed away.",
                "The machines still hum like nothing happened."
            });

        if (loop <= 6)
            return GetRandom(new string[]
            {
                "The samples do not match the story in the files.",
                "The locked cabinet is protecting more than research."
            });

        return GetRandom(new string[]
        {
            "The sequence 4, 2, 7 leads to the cabinet. Isla opens the final truth.",
            "Project Chimera was not a cure. It was a cover."
        });
    }

    private string GetRandom(string[] options)
    {
        return options[Random.Range(0, options.Length)];
    }
}