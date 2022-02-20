using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public TextMeshProUGUI goldText;

    public string[] itemsForSale;
    public ItemButton[] buyButtons;
    public ItemButton[] sellButtons;

    public Item selectedItem;
    public TextMeshProUGUI buyItemName, buyItemDescription, buyItemValue;
    public TextMeshProUGUI sellItemName, sellItemDescription, sellItemValue;

    [Range(0,1)]
    public float sellingFactor;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();
        GameManager.instance.shopActive = true;
        goldText.text = "Gold: " + GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        buyItemName.text = "";
        buyItemDescription.text = "";
        buyItemValue.text = "";
        selectedItem = null;

        for (int i = 0; i < buyButtons.Length; i++)
        {
            buyButtons[i].btnValue = i;

            if (itemsForSale[i] != "")
            {
                buyButtons[i].btnImage.gameObject.SetActive(true);
                buyButtons[i].btnImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyButtons[i].amountText.text = "";
            }
            else
            {
                buyButtons[i].btnImage.gameObject.SetActive(false);
                buyButtons[i].amountText.text = "";
            }
        }
    }

    public void OpenSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        sellItemName.text = "";
        sellItemDescription.text = "";
        sellItemValue.text = "";
        selectedItem = null;

        ShowSellItems();
    }

    public void SelectBuyItem(Item itemToBuy)
    {
        selectedItem = itemToBuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = "Value: " + selectedItem.cost.ToString() + "g";
    }
    public void SelectSellItem(Item itemToSell)
    {
        selectedItem = itemToSell;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.cost * sellingFactor).ToString() + "g";
    }

    public void BuyItem()
    {
        if (selectedItem == null)
            return;
        if (GameManager.instance.currentGold >= selectedItem.cost)
        {
            GameManager.instance.currentGold -= selectedItem.cost;
            GameManager.instance.AddItem(selectedItem.itemName);
        }

        goldText.text = "Gold: " + GameManager.instance.currentGold.ToString() + "g";
    }

    public void SellItem()
    {
        if (selectedItem == null)
            return;

        GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.cost * sellingFactor);
        GameManager.instance.RemoveItem(selectedItem.itemName);
        goldText.text = "Gold: " + GameManager.instance.currentGold.ToString() + "g";

        ShowSellItems();
        if (!GameManager.instance.IsItemInInventory(selectedItem.itemName))
        {
            sellItemName.text = "";
            sellItemDescription.text = "";
            sellItemValue.text = "";
            selectedItem = null;
        }
    }

    private void ShowSellItems()
    {
        //GameManager.instance.SortItems();
        for (int i = 0; i < sellButtons.Length; i++)
        {
            sellButtons[i].btnValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellButtons[i].btnImage.gameObject.SetActive(true);
                sellButtons[i].btnImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellButtons[i].btnImage.gameObject.SetActive(false);
                sellButtons[i].amountText.text = "";
            }
        }
    }
}
