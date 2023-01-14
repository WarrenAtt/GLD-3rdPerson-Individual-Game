using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("UI")]
    public Text MoneyText;

    private void Start()
    {
        if(MoneyText == null)
            MoneyText = GameObject.Find("TotalMoney").GetComponent<Text>();

        this.transform.position = new Vector3(-19.8863125f, 1.565609872f, -4.18821764f);
    }

    // Update is called once per frame
    private void Update()
    {
        MoneyText.text = "Money: $ " + GameData.Money.ToString();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SellItem();
        }

        GameData.PlayerPos = transform.position;
    }

    private bool SellItem()
    {
        Item recievedItem = InventoryManager.inventoryManager.GetSelectedItem(true);

        if (recievedItem != null)
        {
            Debug.Log("Item: " + recievedItem.name + " sold!");

            foreach(Item item in InventoryManager.inventoryManager.itemsAvailable)
            {
                if(item == recievedItem)
                {
                    item.quantity--;
                }
            }

            GameData.Money += recievedItem.sellValue;
        }
        else
        {
            Debug.Log("No item sold!");
        }

        print(GameData.Money);
        return recievedItem;
    }
}
