using UnityEngine;
using TMPro;

public class LevelDetailPanel : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI levelNumberText;    // "Level 01"
    public TextMeshProUGUI identityText;       // "Identity: Professor Kyryll Flins"
    public TextMeshProUGUI descriptionText;    // The atmospheric paragraph

    public void Display(LevelData data)
    {
        levelNumberText.text = $"Level {data.levelNumber:D2}";
        identityText.text = $"Identity: {data.identityName}";
        descriptionText.text = data.description;
    }

    public void Clear()
    {
        levelNumberText.text = "";
        identityText.text = "";
        descriptionText.text = "";
    }
}