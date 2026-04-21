using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "UnfinishedMystery/Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    public string itemId;
    public string displayName;

    [TextArea]
    public string description;

    [Header("Visual")]
    public Sprite icon;

    [Header("Behavior")]
    public bool consumeOnUse = true;
}