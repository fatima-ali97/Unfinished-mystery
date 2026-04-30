using UnityEngine;

namespace InventoryFramework
{
    public class ItemPickupHandler : MonoBehaviour
    {
        public Hotbar hotbar;
        public Inventory inventory;

        public void PickupItem(Item item, int amount = 1)
        {
            Debug.Log("Trying to pick up: " + item.name + " amount: " + amount);

            bool addedToHotbar = hotbar.AddItem(item, amount);
            Debug.Log("Added to hotbar? " + addedToHotbar);

            if (!addedToHotbar)
            {
                bool addedToInventory = inventory.AddItem(item, amount);
                Debug.Log("Added to inventory? " + addedToInventory);

                if (!addedToInventory)
                {
                    Debug.Log("Both hotbar and inventory full!");
                }
            }

            FindAnyObjectByType<HotbarUI>()?.RefreshUI();
            FindAnyObjectByType<InventoryUI>()?.RefreshUI();
        }
    }
}