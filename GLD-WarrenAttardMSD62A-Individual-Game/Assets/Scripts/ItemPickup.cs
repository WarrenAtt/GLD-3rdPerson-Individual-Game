using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemScriptableObject Item;

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
