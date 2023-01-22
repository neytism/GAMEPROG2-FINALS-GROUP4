using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    private void OnEnable()
    {
        PlayerHealth.PlayerDeath += ResetWeapon;
        UserInterfaceManager.ResetWeaponToDefault += ResetWeapon;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDeath -= ResetWeapon;
        UserInterfaceManager.ResetWeaponToDefault -= ResetWeapon;
    }

    public Sprite _icon;
    public string weaponName = "default";
    public BulletType bulletType;
    public float damage = 10;
    public float fireRate = 0.5f;
    public float reloadSpeed = 2f;
    public float bulletSpeed = 50f;
    public float spreadAngle = 5f;
    public int projectiles = 1;
    public int maxPiercing = 1;
    public float knockBackForce = 2f;
    public int maxAmmo = 10;
    public int currentAmmo;

    public Weapon defaultValues;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    public void ResetWeapon()
    {
        damage = defaultValues.damage;
        fireRate = defaultValues.fireRate;
        reloadSpeed = defaultValues.reloadSpeed;
        bulletSpeed = defaultValues.bulletSpeed;
        spreadAngle = defaultValues.spreadAngle;
        projectiles = defaultValues.projectiles;
        maxPiercing = defaultValues.maxPiercing;
        knockBackForce = defaultValues.knockBackForce;
        maxAmmo = defaultValues.maxAmmo;
    }

    public enum BulletType
    {
        normal,
        exploding
    }
    
    
    
}
