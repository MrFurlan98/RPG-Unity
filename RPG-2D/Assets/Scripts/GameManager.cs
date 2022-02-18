using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] charStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
            PlayerController.instance.canMove = false;
        else
            PlayerController.instance.canMove = true;
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }
        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;
                    if (itemsHeld[i] != "")
                        itemAfterSpace = true;
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int slotIndex = 0;
        bool slotFound = false;
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                slotIndex = i;
                slotFound = true;
                break;
            }
        }
        if (slotFound)
        {
            bool itemExists = false;
            for (int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    break;
                }
            }
            if (itemExists)
            {
                itemsHeld[slotIndex] = itemToAdd;
                numberOfItems[slotIndex]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " Does Not Exist");
            }

        }
        GameMenu.instance.ShowItens();
    }

    public void RemoveItem(string itemToRemove)
    {
        int slotIndex = 0;
        bool slotFound = false;
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                slotIndex = i;
                slotFound = true;
                break;
            }
        }
        if (slotFound)
        {
            numberOfItems[slotIndex]--;
            if (numberOfItems[slotIndex] <= 0)
                itemsHeld[slotIndex] = "";
            GameMenu.instance.ShowItens();
        }
        else
        {
            Debug.LogError(itemToRemove + " Not Found");
        }
    }
}
