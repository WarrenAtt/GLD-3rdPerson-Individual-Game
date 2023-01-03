using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void Pickup()
    {
        bool result = InventoryManager.inventoryManager.AddItem(item);

        if (result == true)
        {
            Debug.Log("Item Added");
            ItemRandomSpawner.itemRandomSpawner.currentItemsCount--;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Item not Added!");
        }

        
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}