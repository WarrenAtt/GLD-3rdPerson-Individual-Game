using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    ItemScriptableObject item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        //Increase Money

        Destroy(gameObject);
    }

    public void AddItem(ItemScriptableObject newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.type)
        {
            case ItemScriptableObject.Type.Wood:
                break;
            case ItemScriptableObject.Type.Iron:
                break;
            case ItemScriptableObject.Type.Pickaxe:
                break;
            case ItemScriptableObject.Type.Axe:
                break;
        }
    }
}
