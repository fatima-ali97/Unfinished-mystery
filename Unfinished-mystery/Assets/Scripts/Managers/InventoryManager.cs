using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Settings")]
    [SerializeField] private int maxSlots = 10;
    [SerializeField] private InventorySlotUI[] slotUIs;

    private ItemData[] items;
    private int selectedIndex = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        items = new ItemData[maxSlots];
        RefreshUI();
    }

    private void Update()
    {
        HandleSlotInput();
    }

    private void HandleSlotInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SelectSlot(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) SelectSlot(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) SelectSlot(7);
        if (Input.GetKeyDown(KeyCode.Alpha9)) SelectSlot(8);
        if (Input.GetKeyDown(KeyCode.Alpha0)) SelectSlot(9);
    }

    public bool AddItem(ItemData item)
    {
        if (item == null) return false;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;

                if (selectedIndex == -1)
                    selectedIndex = i;

                RefreshUI();
                return true;
            }
        }

        Debug.Log("Inventory full.");
        return false;
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= items.Length) return;

        selectedIndex = index;
        RefreshUI();
    }

    public ItemData GetSelectedItem()
    {
        if (selectedIndex < 0 || selectedIndex >= items.Length) return null;
        return items[selectedIndex];
    }

    public void RemoveSelectedItem()
    {
        if (selectedIndex < 0 || selectedIndex >= items.Length) return;

        items[selectedIndex] = null;
        selectedIndex = FindFirstFilledSlot();
        RefreshUI();
    }

    public void ClearAllItems()
    {
        for (int i = 0; i < items.Length; i++)
            items[i] = null;

        selectedIndex = -1;
        RefreshUI();
    }

    private int FindFirstFilledSlot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
                return i;
        }

        return -1;
    }

    private void RefreshUI()
    {
        if (slotUIs == null || slotUIs.Length == 0) return;

        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (slotUIs[i] == null) continue;

            ItemData item = i < items.Length ? items[i] : null;
            slotUIs[i].SetItem(item);
            slotUIs[i].SetSelected(i == selectedIndex);
        }
    }
}