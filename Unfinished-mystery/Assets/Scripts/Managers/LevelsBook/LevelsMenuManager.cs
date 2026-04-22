using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuManager : MonoBehaviour
{
    [Header("Data - drag your LevelData assets here")]
    public LevelData[] levels;

    [Header("References")]
    public Transform levelListContent;      // The Content child of your ScrollView
    public GameObject levelRowPrefab;       // Drag your prefab here
    public LevelDetailPanel detailPanel;

    private LevelRowUI[] spawnedRows;
    private int selectedIndex = -1;

    void Start()
    {
        SpawnRows();
        // Auto-select first unlocked level
        for (int i = 0; i < levels.Length; i++)
        {
            if (!levels[i].isLocked)
            {
                OnRowClicked(i);
                break;
            }
        }
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
    }

    // Called when player clicks a row (highlights it, updates right panel)
    public void OnRowClicked(int index)
    {
        if (levels[index].isLocked) return;

        // Deselect previous
        if (selectedIndex >= 0)
            spawnedRows[selectedIndex].SetSelected(false);

        selectedIndex = index;
        spawnedRows[selectedIndex].SetSelected(true);

        // Update right panel
        detailPanel.Display(levels[index]);
    }

    // Called when player clicks Enter button — loads the level scene
    public void OnLevelSelected(int index)
    {
        if (levels[index].isLocked) return;

        Debug.Log($"Entering level {index + 1}: {levels[index].levelTitle}");
        // Replace "Level_01_Scene" with your actual scene names
        SceneManager.LoadScene($"Level_0{index + 1}_Scene");
    }
}