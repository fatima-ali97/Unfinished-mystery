using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<string> collectedReels = new List<string>();
    public TMP_Text inventoryText;

    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void AddReel(string reelName)
    {
        if (!collectedReels.Contains(reelName))
        {
            collectedReels.Add(reelName);
            UpdateUI();
        }
    }

    public bool HasReel(string reelName)
    {
        return collectedReels.Contains(reelName);
    }

    private void UpdateUI()
    {
        if (inventoryText == null) return;

        if (collectedReels.Count == 0)
        {
            inventoryText.text = "Inventory: Empty";
            return;
        }

        inventoryText.text = "Inventory:\n";
        foreach (string reel in collectedReels)
        {
            inventoryText.text += "- " + reel + "\n";
        }
    }
}