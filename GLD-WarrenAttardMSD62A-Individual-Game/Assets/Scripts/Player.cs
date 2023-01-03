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
    }

    // Update is called once per frame
    private void Update()
    {
        MoneyText.text = "Money: $ " + GameData.Money.ToString();


        if (Input.GetKeyDown(KeyCode.Return))
        {
            SellItem();
        }
    }

    private bool SellItem()
    {
        Item recievedItem = InventoryManager.inventoryManager.GetSelectedItem(true);

        if (recievedItem != null)
        {
            Debug.Log("Item: " + recievedItem.name + " sold!");
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
