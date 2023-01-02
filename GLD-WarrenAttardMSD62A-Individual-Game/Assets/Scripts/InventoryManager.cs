using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemScriptableObject> Items; 

    public Transform ItemContent;
    public GameObject inventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Items = new List<ItemScriptableObject>();
    }

    public void Add(ItemScriptableObject addedItem)
    {
        int countItems = Items.Where(x => x == addedItem).ToList().Count;

        if(countItems == 0)
        {
            addedItem.quantity = 1;
            Items.Add(addedItem);
        }
        else
        {
            foreach(ItemScriptableObject item in Items)
            {
                if(item == addedItem)
                {
                    item.quantity += 1;
                }
            }
        }
    }

    public void Remove(ItemScriptableObject item)
    {
        foreach (ItemScriptableObject obj in Items)
        {
            print(item);
            if (obj == item)
            {
                if (Items.Count != 0)
                {
                    obj.quantity -= 1;

                    if (obj.quantity == 0)
                    {
                        Items.Remove(obj);
                    }
                }
            }
        }
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            
            DestroyImmediate(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(inventoryItem, ItemContent);

            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemQuantity = obj.transform.Find("ItemQuantity").GetComponent<TextMeshProUGUI>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.name;
            itemIcon.sprite = item.icon;
            itemQuantity.text = "x" + item.quantity.ToString();

            if (EnableRemove.isOn)
                removeButton.gameObject.SetActive(true);
        }

        SetIventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetIventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        print(Items.Count);
        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
