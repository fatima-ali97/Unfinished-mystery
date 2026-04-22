using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LevelRowUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("References")]
    public TextMeshProUGUI titleText;
    public Button enterButton;
    public GameObject enterButtonGroup;
    public GameObject selectionBorder;

    [HideInInspector] public LevelData data;
    [HideInInspector] public int index;

    private LevelsMenuManager manager;

    public void Setup(LevelData levelData, int rowIndex, LevelsMenuManager mgr)
    {
        data = levelData;
        index = rowIndex;
        manager = mgr;

        titleText.text = $"Level {levelData.levelNumber:D2}: {levelData.levelTitle}";

        SetLocked(levelData.isLocked);
        SetSelected(false);

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

        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg != null)
            cg.alpha = locked ? 0.5f : 1f;
    }

    // --- Hover Events ---

    public void OnPointerEnter(PointerEventData eventData)
    {
        manager.OnRowHovered(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        manager.OnRowUnhovered(index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.OnRowClicked(index);
    }

    void OnEnterClicked()
    {
        manager.OnLevelSelected(index);
    }
}