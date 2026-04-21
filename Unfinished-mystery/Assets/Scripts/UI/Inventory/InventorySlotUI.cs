using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectedFrame;

    public void SetItem(ItemData item)
    {
        if (item == null)
        {
            iconImage.enabled = false;
            iconImage.sprite = null;
            return;
        }

        iconImage.enabled = true;
        iconImage.sprite = item.icon;
    }

    public void SetSelected(bool isSelected)
    {
        if (selectedFrame != null)
            selectedFrame.SetActive(isSelected);
    }
}