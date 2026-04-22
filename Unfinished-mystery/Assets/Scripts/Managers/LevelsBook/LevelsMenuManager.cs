using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuManager : MonoBehaviour
{
    [Header("Data")]
    public LevelData[] levels;

    [Header("References")]
    public Transform levelListContent;
    public GameObject levelRowPrefab;
    public LevelDetailPanel detailPanel;

    private LevelRowUI[] spawnedRows;
    private int selectedIndex = -1;   // clicked/locked selection
    private int hoveredIndex = -1;    // currently hovered

    void Start()
    {
        SpawnRows();
    }

    void SpawnRows()
    {
        spawnedRows = new LevelRowUI[levels.Length];

        for (int i = 0; i < levels.Length; i++)
        {
            GameObject rowGO = Instantiate(levelRowPrefab, levelListContent);
            LevelRowUI row = rowGO.GetComponent<LevelRowUI>();
            row.Setup(levels[i], i, this);
            spawnedRows[i] = row;
        }
        
        // Auto-select first level by default
        OnRowClicked(0);
    }

    // Called when mouse enters a row
    public void OnRowHovered(int index)
    {
        if (levels[index].isLocked) return;

        hoveredIndex = index;

        // Show border on hovered row (unless a different row is selected)
        if (selectedIndex != index)
            spawnedRows[index].SetSelected(true);

        // Show details on right panel
        detailPanel.Display(levels[index]);
    }

    // Called when mouse leaves a row
    public void OnRowUnhovered(int index)
    {
        hoveredIndex = -1;

        // Remove border only if this row isn't the selected one
        if (selectedIndex != index)
            spawnedRows[index].SetSelected(false);

        // Restore selected row's details, or clear if nothing selected
        if (selectedIndex >= 0)
            detailPanel.Display(levels[selectedIndex]);
        else
            detailPanel.Clear();
    }

    // Called when a row is clicked — locks the selection
    public void OnRowClicked(int index)
    {
        if (levels[index].isLocked) return;

        // Deselect previous
        if (selectedIndex >= 0)
            spawnedRows[selectedIndex].SetSelected(false);

        selectedIndex = index;
        spawnedRows[selectedIndex].SetSelected(true);
        detailPanel.Display(levels[index]);
    }

    // Called when Enter button is clicked — loads the scene
    public void OnLevelSelected(int index)
    {
        if (levels[index].isLocked) return;

        SceneManager.LoadScene($"Level_0{index + 1}_Scene");
    }
}