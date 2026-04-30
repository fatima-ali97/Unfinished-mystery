using UnityEngine;

namespace InventoryFramework
{
    public class PickupItem : MonoBehaviour
    {
        public Item item;
        public int amount = 1;

        private bool playerInRange = false;
        private ItemPickupHandler pickupHandler;

        private void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                if (pickupHandler != null && item != null)
                {
                    pickupHandler.PickupItem(item, amount);
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = true;
                pickupHandler = other.GetComponent<ItemPickupHandler>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
                pickupHandler = null;
            }
        }
    }
}