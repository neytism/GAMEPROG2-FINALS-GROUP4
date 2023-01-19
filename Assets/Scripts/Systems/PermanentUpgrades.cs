using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Permanent Upgrade")]
public class PermanentUpgrades : ScriptableObject
{
    //description
    public Sprite _icon;
    public string upgradeName;
    [TextArea] public string upgradeDescription;
    
    //data
    public float requiredOrbs;
    public float depositedOrbs;
    public bool isUnlocked;
    public PermanentUpgrades[] prerequisites;

    //player upgrades
    public int addMaxHealth;
    public int addCurrentHealth;
    public float percentMovementSpeed;
    public float percentPickUpRange;

    //weapon upgrades
    public Weapon.BulletType ChangeBulletType;
    public int addFlatDamage;
    public float percentDamage;
    public float percentFireRate;
    public float percentReloadSpeed;
    public float percentBulletSpeed;
    public float percentSpreadAngle;
    public int addProjectiles;
    public int addMaxPiercing;
    public float percentKnockBack;
    public int addMaxAmmo;

    public void DepositOrbs()
    {
        depositedOrbs++;

        if (depositedOrbs >= requiredOrbs)
        {
            depositedOrbs = requiredOrbs;
            Unlock();
        }
    }

    public void Unlock()
    {
        if (CheckPrerequisites())
        {
            isUnlocked = true;
            //apply effects here
        }
    }

    private bool CheckPrerequisites()
    {
        if (prerequisites.Length == 0)
        {
            return true;
        }

        foreach (PermanentUpgrades prerequisite in prerequisites)
        {
            if (!prerequisite.isUnlocked)
            {
                return false;
            }
        }

        return true;
    }
    
}
