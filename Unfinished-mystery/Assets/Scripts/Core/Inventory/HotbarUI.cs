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

        public Item drawerKeyItem;

        private List<InventorySlotUI> slotUIs = new();
        private int selectedIndex = 0;

        public RectTransform dragLayer;
        public Canvas rootCanvas;

        void Start()
        {
            slotUIs = new List<InventorySlotUI>();

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
                Debug.LogWarning($"HotbarUI: slot count in slotParent = {slotUIs.Count}, but hotbar.size = {hotbar.size}.");
            }

            // يحط المفتاح في أول خانة من الهوتبار
            if (drawerKeyItem != null && hotbar != null && hotbar.slots != null && hotbar.slots.Count > 0)
            {
                hotbar.slots[0].item = drawerKeyItem;
                hotbar.slots[0].count = 1;
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

                Image bg = slotUIs[i].GetBackgroundImage();
                if (bg != null)
                {
                    bg.color = (i == selectedIndex) ? Color.yellow : Color.white;
                }
            }

            if (toolsParent == null) return;
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