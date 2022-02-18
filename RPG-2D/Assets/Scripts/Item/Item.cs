using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;
    [Header("Item Details")]
    public string itemName;
    public string itemDescription;
    public int cost;
    public Sprite itemSprite;
    [Header("Item Effects")]
    public int effectValue;
    public bool effectHP, effectMP, effectSTR, effectDEF;
    [Header("Weapon/Armor Details")]
    public int wpnStrength, armrStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUseOn)
    {
        CharStats selectChar = GameManager.instance.charStats[charToUseOn];

        if (isItem)
        {
            if (effectHP)
                selectChar.currentHP = Mathf.Clamp(selectChar.currentHP + effectValue,selectChar.currentHP,selectChar.maxHP);
            if (effectMP)
                selectChar.currentMP = Mathf.Clamp(selectChar.currentMP + effectValue,selectChar.currentMP,selectChar.maxMP);
            if (effectSTR)
                selectChar.strength += effectValue;
            if (effectDEF)
                selectChar.defence += effectValue;
        }
        if (isWeapon)
        {
            if(selectChar.equippedWpn != "")
                GameManager.instance.AddItem(selectChar.equippedWpn);

            selectChar.equippedWpn = itemName;
            selectChar.wpnPwr = wpnStrength;
        }
        if (isArmor)
        {
            if (selectChar.equippedArmr != "")
                GameManager.instance.AddItem(selectChar.equippedArmr);

            selectChar.equippedArmr = itemName;
            selectChar.armrPwr = armrStrength;
        }
        GameManager.instance.RemoveItem(itemName);
    }
}
