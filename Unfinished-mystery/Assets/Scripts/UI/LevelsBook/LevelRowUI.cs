using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelRowUI : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI loopsText;
    public Button enterButton;
    public GameObject selectionBorder;   // The orange outline Image
    public GameObject lockedIndicator;   // "Locked" label + icon
    public GameObject enterButtonGroup;  // The red Enter button

    [HideInInspector] public LevelData data;
    [HideInInspector] public int index;

    private LevelsMenuManager manager;

    public void Setup(LevelData levelData, int rowIndex, LevelsMenuManager mgr)
    {
        data = levelData;
        index = rowIndex;
        manager = mgr;

        titleText.text = $"Level {levelData.levelNumber:D2}: {levelData.levelTitle}";
        loopsText.text = $"Loops Remaining: {levelData.loopsRemaining}";

        SetLocked(levelData.isLocked);
        SetSelected(false);

        // Wire the Enter button
        enterButton.onClick.RemoveAllListeners();
        enterButton.onClick.AddListener(OnEnterClicked);
    }

    public void SetSelected(bool selected)
    {
        selectionBorder.SetActive(selected);
    }

    public void SetLocked(bool locked)
    {
        enterButtonGroup.SetActive(!locked);
        lockedIndicator.SetActive(locked);

        // Dim the whole row if locked
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg != null)
            cg.alpha = locked ? 0.5f : 1f;
    }

    void OnEnterClicked()
    {
        manager.OnLevelSelected(index);
    }

    // Called when the row itself is clicked (to show detail on right panel)
    public void OnRowClicked()
    {
        manager.OnRowClicked(index);
    }
}