using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace InventoryFramework
{
    public enum SlotOwner
    {
        Inventory,
        Hotbar,
    }

    public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image backgroundImage;
        public Image icon;
        public TextMeshProUGUI countText;

        Inventory inventory;
        InventoryUI inventoryUI;

        Hotbar hotbar;
        HotbarUI hotbarUI;

        public int index;
        public SlotOwner owner;

        GameObject dragIcon;
        RectTransform dragRT;

        public ItemTooltip tooltip;

        void Update()
        {
            if (tooltip != null && tooltip.gameObject.activeSelf)
            {
                tooltip.UpdatePosition(Input.mousePosition);
            }
        }

        public void Setup(Inventory inv, Hotbar hb, int idx, InventoryUI ui)
        {
            inventory = inv;
            hotbar = hb;
            index = idx;
            inventoryUI = ui;
            owner = SlotOwner.Inventory;
            hotbarUI = null;
        }

        public void SetupHotbar(Hotbar hb, Inventory inv, int idx, HotbarUI ui)
        {
            hotbar = hb;
            inventory = inv;
            hotbarUI = ui;
            index = idx;
            owner = SlotOwner.Hotbar;
            inventoryUI = null;
        }

        public InventorySlot GetSlot()
        {
            return owner == SlotOwner.Inventory ? inventory.slots[index] : hotbar.slots[index];
        }

        public Image GetBackgroundImage()
        {
            return backgroundImage;
        }

        public void SetSlot(InventorySlot slot)
        {
            if (slot == null || slot.IsEmpty || slot.item == null || slot.item.icon == null)
            {
                if (icon != null)
                {
                    icon.sprite = null;
                    icon.enabled = false;
                    icon.color = new Color(1f, 1f, 1f, 0f);
                }

                if (countText != null)
                {
                    countText.text = "";
                }
            }
            else
            {
                if (icon != null)
                {
                    icon.enabled = true;
                    icon.sprite = slot.item.icon;
                    icon.color = Color.white;
                }

                if (countText != null)
                {
                    countText.text = slot.count > 1 ? slot.count.ToString() : "";
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var s = GetSlot();
            if (s == null || s.IsEmpty) return;

            int amount;

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                amount = Mathf.CeilToInt(s.count / 2f);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                amount = 1;
            }
            else
            {
                amount = s.count;
            }

            DragContext.draggedItem = s.item;
            DragContext.draggedCount = amount;
            DragContext.fromSlotIndex = index;
            DragContext.fromOwner = owner;

            s.count -= amount;
            if (s.count <= 0)
            {
                s.item = null;
                s.count = 0;
            }

            Transform parentLayer = owner == SlotOwner.Inventory ? inventoryUI.dragLayer : hotbarUI.dragLayer;

            dragIcon = new GameObject("DragIcon");
            dragIcon.transform.SetParent(parentLayer, false);

            dragRT = dragIcon.AddComponent<RectTransform>();
            var img = dragIcon.AddComponent<Image>();
            img.sprite = icon.sprite;
            img.raycastTarget = false;
            dragRT.sizeDelta = icon.rectTransform.sizeDelta;

            RefreshAllUIs();
            UpdateDragPosition(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragIcon == null) return;
            UpdateDragPosition(eventData);
        }

        void UpdateDragPosition(PointerEventData eventData)
        {
            Vector2 localPoint;
            Canvas canvas = null;
            RectTransform dragLayer = null;

            if (owner == SlotOwner.Inventory && inventoryUI != null)
            {
                canvas = inventoryUI.rootCanvas;
                dragLayer = inventoryUI.dragLayer;
            }
            else if (owner == SlotOwner.Hotbar && hotbarUI != null)
            {
                canvas = hotbarUI.rootCanvas;
                dragLayer = hotbarUI.dragLayer;
            }
            else
            {
                Debug.LogError("No valid UI context for drag!");
                return;
            }

            Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(dragLayer, eventData.position, cam, out localPoint);
            dragRT.anchoredPosition = localPoint;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (dragIcon != null) Destroy(dragIcon);

            if (DragContext.draggedItem != null && DragContext.draggedCount > 0)
            {
                var originalSlot = GetOriginalSlot();
                if (originalSlot != null)
                {
                    if (originalSlot.IsEmpty)
                    {
                        originalSlot.item = DragContext.draggedItem;
                        originalSlot.count = DragContext.draggedCount;
                    }
                    else if (originalSlot.item == DragContext.draggedItem)
                    {
                        originalSlot.count += DragContext.draggedCount;
                    }
                }

                DragContext.draggedItem = null;
                DragContext.draggedCount = 0;
            }

            RefreshAllUIs();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (DragContext.draggedItem == null || DragContext.draggedCount <= 0) return;

            var targetSlot = GetSlot();
            if (targetSlot == null) return;

            if (targetSlot.IsEmpty)
            {
                targetSlot.item = DragContext.draggedItem;
                targetSlot.count = DragContext.draggedCount;
            }
            else if (targetSlot.item == DragContext.draggedItem)
            {
                int space = targetSlot.item.maxStack - targetSlot.count;
                int add = Mathf.Min(space, DragContext.draggedCount);
                targetSlot.count += add;
                DragContext.draggedCount -= add;

                if (DragContext.draggedCount > 0)
                {
                    ReturnToOriginalSlot();
                }
            }
            else
            {
                var originalSlot = GetOriginalSlot();
                if (originalSlot != null)
                {
                    var tmpItem = targetSlot.item;
                    var tmpCount = targetSlot.count;

                    targetSlot.item = DragContext.draggedItem;
                    targetSlot.count = DragContext.draggedCount;

                    originalSlot.item = tmpItem;
                    originalSlot.count = tmpCount;
                }
            }

            DragContext.draggedItem = null;
            DragContext.draggedCount = 0;

            RefreshAllUIs();
        }

        private InventorySlot GetOriginalSlot()
        {
            switch (DragContext.fromOwner)
            {
                case SlotOwner.Inventory:
                    return inventory.slots[DragContext.fromSlotIndex];

                case SlotOwner.Hotbar:
                    return hotbar.slots[DragContext.fromSlotIndex];

                default:
                    Debug.LogError("Invalid DragContext.fromOwner: " + DragContext.fromOwner);
                    return null;
            }
        }

        private void ReturnToOriginalSlot()
        {
            var originalSlot = GetOriginalSlot();
            if (originalSlot == null) return;

            if (originalSlot.IsEmpty)
            {
                originalSlot.item = DragContext.draggedItem;
                originalSlot.count = DragContext.draggedCount;
            }
            else if (originalSlot.item == DragContext.draggedItem)
            {
                originalSlot.count += DragContext.draggedCount;
            }
        }

        private void RefreshAllUIs()
        {
            if (inventoryUI != null) inventoryUI.RefreshUI();
            if (hotbarUI != null) hotbarUI.RefreshUI();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var slot = GetSlot();
            if (slot != null && !slot.IsEmpty && tooltip != null)
            {
                tooltip.Show(slot.item, eventData.position);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (tooltip != null)
            {
                tooltip.Hide();
            }
        }
    }
}