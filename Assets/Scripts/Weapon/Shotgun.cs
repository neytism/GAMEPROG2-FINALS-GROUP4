using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public float _additionalDamageUpgrade;
    
    [SerializeField] private Weapon _weaponDefault;

    public Weapon localWeapon;

    private void Awake()
    {
        //gets data from save
        _additionalDamageUpgrade = SaveSystem.Instance.shotgunAdditionalDamage;
        
        //puts default weapon data on current weapon data to get unchanged data
        localWeapon.damage = _weaponDefault.damage;
        localWeapon.fireRate = _weaponDefault.fireRate;
        localWeapon.reloadSpeed = _weaponDefault.reloadSpeed;
        localWeapon.bulletSpeed = _weaponDefault.bulletSpeed;
        localWeapon.spreadAngle = _weaponDefault.spreadAngle;
        localWeapon.projectiles = _weaponDefault.projectiles;
        localWeapon.maxPiercing = _weaponDefault.maxPiercing;
        localWeapon.knockBackForce = _weaponDefault.knockBackForce;
        localWeapon.maxAmmo = _weaponDefault.maxAmmo;

    }

    private void OnEnable()
    {
        
        WeaponHolder.WeaponEquipped += InitializeWeapon;
    }

    private void OnDisable()
    {
        WeaponHolder.WeaponEquipped -= InitializeWeapon;
    }

    private void InitializeWeapon()
    {
        //applies changes from save to the local data
        localWeapon.damage = _additionalDamageUpgrade + _weaponDefault.damage;
    }
    
}
