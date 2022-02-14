using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int charLevel = 1;
    public int maxLevel = 20;
    public int currentEXP = 0;
    public int[] expToNextLevel;
    public int baseExp = 1000;

    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int[] mpLvlBonus;
    public int strength;
    public int defense;
    public int wpnPwr;
    public int armrPwr;
    public string equippedWpn;
    public string equippedArmr;
    public Sprite charImage;
    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = 1000; 

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEXP(int expToAdd)
    {
        currentEXP += expToAdd;
        if(charLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[charLevel])
            {
                currentEXP -= expToNextLevel[charLevel];
                charLevel++;

                if (charLevel % 2 == 0)
                    strength++;
                else
                    defense++;

                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                maxMP += mpLvlBonus[charLevel];
                currentMP = maxMP;
            }
        }

        if (charLevel >= maxLevel)
            currentEXP = 0;
    }
}
