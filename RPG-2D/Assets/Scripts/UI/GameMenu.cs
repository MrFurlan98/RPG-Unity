using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public GameObject menu;
    public GameObject[] windows;

    private CharStats[] playerStats;

    public TextMeshProUGUI[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] statsBtns;

    public TextMeshProUGUI statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, statusArmrPwr, statusEXP;
    public Image statusImage;

    public ItemButton[] itemButtons;
    public string itemSelected;
    public Item activeItem;
    public TextMeshProUGUI activeItemName, activeItemDescription, useBtnText;

    public GameObject ItemCharPanel;
    public TextMeshProUGUI[] itemCharChoiceNames;

    public TextMeshProUGUI goldText;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeInHierarchy)
                CloseMenu();
            else
            {
                menu.SetActive(!menu.activeInHierarchy);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = menu.activeInHierarchy;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.charStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Lvl : " + playerStats[i].charLevel;
                expText[i].text = "EXP: " + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].charLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].charLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }

        goldText.text = "Gold: " + GameManager.instance.currentGold.ToString() + "g";
    }

    public void ToggleWindow(int windowNum)
    {
        UpdateMainStats();
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNum)
                windows[i].SetActive(!windows[i].activeInHierarchy);
            else
                windows[i].SetActive(false);
        }
        ItemCharPanel.SetActive(false);
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        menu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
        ItemCharPanel.SetActive(false);
    }

    public void OpenStats()
    {
        UpdateMainStats();

        StatusCharacter(0);

        for (int i = 0; i < statsBtns.Length; i++)
        {
            statsBtns[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statsBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].charName;
        }
    }

    public void StatusCharacter(int selected)
    {
        statusName.text = playerStats[selected].charName;
        statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defence.ToString();
        if (playerStats[selected].equippedWpn != "")
            statusWpnEqpd.text = playerStats[selected].equippedWpn;
        statusWpnPwr.text = "" + playerStats[selected].wpnPwr;
        if (playerStats[selected].equippedArmr != "")
            statusArmrEqpd.text = playerStats[selected].equippedArmr;
        statusArmrPwr.text = "" + playerStats[selected].armrPwr;
        statusEXP.text = (playerStats[selected].expToNextLevel[playerStats[selected].charLevel] - playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;
    }

    public void ShowItens()
    {
        GameManager.instance.SortItems();
        activeItemDescription.text = "";
        activeItemName.text = "";

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].btnValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].btnImage.gameObject.SetActive(true);
                itemButtons[i].btnImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].btnImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
        if (activeItem.isItem)
            useBtnText.text = "Use";
        if (activeItem.isArmor || activeItem.isWeapon)
            useBtnText.text = "Equip";
        activeItemName.text = activeItem.itemName;
        activeItemDescription.text = activeItem.itemDescription;
    }

    public void DiscardItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        ItemCharPanel.SetActive(true);
        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.charStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.charStats[i].gameObject.activeInHierarchy);
        }
    }
    public void CloseItemCharChoice()
    {
        ItemCharPanel.SetActive(false);
    }

    public void UseItem(int selectedChar)
    {
        activeItem.Use(selectedChar);
        CloseItemCharChoice();
    }
}
