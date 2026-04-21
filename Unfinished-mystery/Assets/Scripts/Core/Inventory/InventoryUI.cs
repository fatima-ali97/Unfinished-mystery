using System.Collections.Generic;
using UnityEngine;

namespace InventoryFramework
{
    public class InventoryUI : MonoBehaviour
    {
        public Inventory inventory;
        public Hotbar hotbar;
        public Transform slotParent;
        public ItemTooltip tooltip;

        public RectTransform dragLayer;
        public Canvas rootCanvas;

        private List<InventorySlotUI> slotUIs;

        void Start()
        {
            slotUIs = new List<InventorySlotUI>();

            foreach (Transform child in slotParent)
            {
                InventorySlotUI ui = child.GetComponent<InventorySlotUI>();

                if (ui != null)
                {
                    ui.tooltip = tooltip;
                    ui.Setup(inventory, hotbar, slotUIs.Count, this);
                    slotUIs.Add(ui);
                }
            }

            if (slotUIs.Count < inventory.size)
            {
                Debug.LogWarning($"InventoryUI: slot count in slotParent = {slotUIs.Count}, but inventory.size = {inventory.size}.");
            }

            RefreshUI();
        }

        public void RefreshUI()
        {
            int count = Mathf.Min(inventory.size, slotUIs.Count);

            for (int i = 0; i < count; i++)
            {
                slotUIs[i].SetSlot(inventory.slots[i]);
            }
        }
    }
}