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

            // ناخذ كل الـ slots الموجودة يدويًا داخل slotParent
            foreach (Transform child in slotParent)
            {
                InventorySlotUI slotUI = child.GetComponent<InventorySlotUI>();

                if (slotUI != null)
                {
                    slotUI.tooltip = tooltip;
                    slotUI.Setup(inventory, hotbar, slotUIs.Count, this);
                    slotUIs.Add(slotUI);
                }
            }

            // تحذير إذا عدد السلوتات اليدوية أقل من حجم الانفنتوري
            if (slotUIs.Count < inventory.size)
            {
                Debug.LogWarning($"InventoryUI: عدد السلوتات الموجودة داخل slotParent هو {slotUIs.Count}, لكن inventory.size = {inventory.size}.");
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