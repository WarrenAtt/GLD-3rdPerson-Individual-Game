using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager inventoryManager = null;
    [Header("Settings")]
    public int maxStackedItems = 15;
    public List<Item> itemsAvailable = new List<Item>();
    public List<Item> Items = new List<Item>();

    [Header("UI")]
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform ContentList;
    public GameObject informationItemPrefab;

    int selectedSlot = -1;

    private void Awake()
    {
        inventoryManager = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
        DisplayItemInformation();
    }

    private void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 8)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();

                GameData.PlayerInvetory = GetItemsInInventory();
                AddItemQuantity(item);
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                GameData.PlayerInvetory = GetItemsInInventory();
                item.quantity = 0;
                AddItemQuantity(item);
                return true;
            }
        }

        return false;
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;

            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }

                return item;
            }
        }

        return null;
    }

    public void CraftItem(Item craftableItem)
    {
        InventoryItem Wood = GetItemInInventory(ItemType.Wood);
        InventoryItem Iron = GetItemInInventory(ItemType.Iron);
        
        if (Wood != null && Iron != null)
        {
            if (craftableItem.type == ItemType.Pickaxe)
            {
                if (Iron.count >= 2 && Wood.count >= 1)
                {
                    ReduceItem(ItemType.Iron, 2);
                    ReduceItem(ItemType.Wood, 1);

                    Debug.Log(craftableItem + " Crafted!");
                    AddItem(craftableItem);
                    return;
                }
            }

            if(craftableItem.type == ItemType.Axe){
                if (Iron.count >= 1 && Wood.count >= 2)
                {
                    ReduceItem(ItemType.Iron, 1);
                    ReduceItem(ItemType.Wood, 2);

                    Debug.Log(craftableItem + " Crafted!");
                    AddItem(craftableItem);
                    return;
                }
            }
        }
    }

    private void ReduceItem(ItemType itemType, int amount)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null)
            {
                if (itemInSlot.item.type == itemType)
                {
                    itemInSlot.count -= amount;
                    if (itemInSlot.count <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                    }
                    else
                    {
                        itemInSlot.RefreshCount();
                    }
                }
            }
        }
    }

    private InventoryItem GetItemInInventory(ItemType itemType)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item.type == itemType)
            {
                return itemInSlot;
            }
        }

        return null;
    }

    private List<Item> GetItemsInInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && !Items.Contains(itemInSlot.item))
            {
                print("test");
                Items.Add(itemInSlot.item);
            }
        }

        return Items;
    }

    private void AddItemQuantity(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && item == itemInSlot.item)
            {
                item.quantity++;
            }
        }
    }

    private void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
            inventorySlots[selectedSlot].Deselect();
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);

        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    private void DisplayItemInformation()
    {
        foreach(Item item in itemsAvailable)
        {
            GameObject obj = Instantiate(informationItemPrefab, ContentList);

            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemCost = obj.transform.Find("ItemCost").GetComponent<TextMeshProUGUI>();

            itemName.text = item.name;
            itemIcon.sprite = item.image;
            itemCost.text = "Sell Value: $" + item.sellValue.ToString();
        }
    }
}
