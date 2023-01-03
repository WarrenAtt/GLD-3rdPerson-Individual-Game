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
        }
        else
        {
            Debug.Log("Item not Added!");
        }

        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}