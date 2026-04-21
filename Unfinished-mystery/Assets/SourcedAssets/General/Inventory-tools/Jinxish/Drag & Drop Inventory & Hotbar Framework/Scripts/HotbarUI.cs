using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryFramework
{
    public class HotbarUI : MonoBehaviour
    {
        public Hotbar hotbar;
        public Inventory inventory;
        public Transform slotParent;
        public ItemTooltip tooltip;
        public Transform toolsParent;

        private List<InventorySlotUI> slotUIs = new();
        private int selectedIndex = 0;

        public RectTransform dragLayer;
        public Canvas rootCanvas;

        void Start()
        {
            slotUIs = new List<InventorySlotUI>();

            // ياخذ السلوتات الموجودة يدويًا داخل slotParent
            foreach (Transform child in slotParent)
            {
                InventorySlotUI ui = child.GetComponent<InventorySlotUI>();

                if (ui != null)
                {
                    ui.tooltip = tooltip;
                    ui.SetupHotbar(hotbar, inventory, slotUIs.Count, this);
                    slotUIs.Add(ui);
                }
            }

            if (slotUIs.Count < hotbar.size)
            {
                Debug.LogWarning($"HotbarUI: عدد السلوتات الموجودة داخل slotParent هو {slotUIs.Count}, لكن hotbar.size = {hotbar.size}.");
            }

            RefreshUI();
        }

        void Update()
        {
            int count = Mathf.Min(hotbar.size, slotUIs.Count);

            for (int i = 0; i < count; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    selectedIndex = i;
                    RefreshUI();
                }
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f && count > 0)
            {
                selectedIndex = (selectedIndex + 1) % count;
                RefreshUI();
            }
            else if (scroll < 0f && count > 0)
            {
                selectedIndex = (selectedIndex - 1 + count) % count;
                RefreshUI();
            }
        }

        public void RefreshUI()
        {
            int count = Mathf.Min(hotbar.size, slotUIs.Count);

            for (int i = 0; i < count; i++)
            {
                slotUIs[i].SetSlot(hotbar.slots[i]);

                Transform bgChild = slotUIs[i].transform.GetChild(0);
                Image bg = bgChild.GetComponent<Image>();

                if (bg != null)
                {
                    bg.color = (i == selectedIndex) ? Color.yellow : Color.white;
                }
            }

            if (selectedIndex >= count) return;

            InventorySlot slot = slotUIs[selectedIndex].GetSlot();

            for (int x = toolsParent.childCount - 1; x >= 0; x--)
            {
                Destroy(toolsParent.GetChild(x).gameObject);
            }

            if (slot == null) return;
            if (slot.IsEmpty) return;
            if (slot.item == null) return;
            if (slot.item.model == null) return;

            Instantiate(slot.item.model, toolsParent);
        }
    }
}