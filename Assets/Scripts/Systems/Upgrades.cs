using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades")]
public class Upgrades : ScriptableObject
{
    //description
    [TextArea] public string upgradeDescription;
    
    //player upgrades
    public int addMaxHealth;
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

}
