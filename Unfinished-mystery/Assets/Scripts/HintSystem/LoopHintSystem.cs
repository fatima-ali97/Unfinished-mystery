using UnityEngine;
using TMPro;

public class LoopHintSystem : MonoBehaviour
{
    public static LoopHintSystem Instance;

    [Header("Level")]
    [Range(1, 5)]
    public int levelNumber = 1;

    [Header("Loop")]
    [Range(1, 10)]
    public int currentLoop = 1;

    [Header("UI")]
    public TMP_Text hintText;
    public GameObject hintPanel;

    private bool hintVisible = false;

    private void Awake()
    {
        Instance = this;

        if (hintPanel != null)
            hintPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            ToggleHint();
    }

    public void SetLoop(int loop)
    {
        currentLoop = Mathf.Clamp(loop, 1, 10);
        RefreshHint();
    }

    public void NextLoop()
    {
        currentLoop = Mathf.Clamp(currentLoop + 1, 1, 10);
        RefreshHint();
    }

    public void SetLevel(int level)
    {
        levelNumber = Mathf.Clamp(level, 1, 5);
        RefreshHint();
    }

    public void ToggleHint()
    {
        hintVisible = !hintVisible;

        if (hintPanel != null)
            hintPanel.SetActive(hintVisible);

        RefreshHint();
    }

    public void ShowHint()
    {
        hintVisible = true;

        if (hintPanel != null)
            hintPanel.SetActive(true);

        RefreshHint();
    }

    public void HideHint()
    {
        hintVisible = false;

        if (hintPanel != null)
            hintPanel.SetActive(false);
    }

    private void RefreshHint()
    {
        if (hintText == null) return;

        hintText.text = GetHint(levelNumber, currentLoop);
    }

    private string GetHint(int level, int loop)
    {
        if (loop <= 3)
            return "No hint available yet. Observe the room carefully.";

        if (level == 1) return Level1Hint(loop);
        if (level == 2) return Level2Hint(loop);
        if (level == 3) return Level3Hint(loop);
        if (level == 4) return Level4Hint(loop);
        if (level == 5) return Level5Hint(loop);

        return "The hint feels unclear.";
    }

    private string Level1Hint(int loop)
    {
        if (loop <= 6)
            return "Some numbers in the professor’s office are not random. Look for a repeated pattern.";

        return "Focus on the Fibonacci clue. F(12) gives the password for the laptop.";
    }

    private string Level2Hint(int loop)
    {
        if (loop <= 6)
            return "The child’s drawing contains numbers. Count what Isla drew.";

        return "Use 259 as the badge clue. After the phone message, check the frozen clock for the final code.";
    }

    private string Level3Hint(int loop)
    {
        if (loop <= 6)
            return "The film reels are not equal. Watch carefully for which scenes connect.";

        return "Find the correct reel sequence. The final reel reveals the missing girl’s truth.";
    }

    private string Level4Hint(int loop)
    {
        if (loop <= 6)
            return "The photographs point toward specific books. Pay attention to highlighted words.";

        return "Convert Fifth, Second, and Eighth into numbers. The drawer code is 528.";
    }

    private string Level5Hint(int loop)
    {
        if (loop <= 6)
            return "The samples, ledger, and cabinet are connected. Look for a repeated sequence.";

        return "Use 4, 2, 7 to open the cabinet. Isla’s name leads to Project Chimera’s truth.";
    }
}