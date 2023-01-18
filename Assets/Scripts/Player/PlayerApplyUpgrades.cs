using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerApplyUpgrades : MonoBehaviour
{
    public static event Action UpgradeApplied;

    private Weapon _weapon;
    private PlayerStats _player;
    public Upgrades upgrade;
    
    public void ApplyUpgrade()
    {
        _player = FindObjectOfType<PlayerStats>();
        _weapon = FindObjectOfType<WeaponController>().Weapon;
        
        //weapon
        _weapon.damage += (_weapon.damage * upgrade.percentDamage);
        _weapon.fireRate += (_weapon.fireRate * upgrade.percentFireRate);
        _weapon.reloadSpeed -= (_weapon.reloadSpeed * upgrade.percentReloadSpeed);
        _weapon.bulletSpeed += (_weapon.bulletSpeed * upgrade.percentBulletSpeed);
        _weapon.spreadAngle += (_weapon.spreadAngle * upgrade.percentSpreadAngle);
        _weapon.knockBackForce += (_weapon.knockBackForce * upgrade.percentKnockBack);
        _weapon.projectiles += upgrade.addProjectiles;
        _weapon.maxPiercing += upgrade.addMaxPiercing;
        _weapon.maxAmmo += upgrade.addMaxAmmo;
        _weapon.damage += upgrade.addFlatDamage;
        
        //player
        _player.MaxHealth += upgrade.addMaxHealth;
        _player.CurrentHealth += upgrade.addCurrentHealth;
        _player.MovementSpeed += (_player.MovementSpeed * upgrade.percentMovementSpeed);
        _player.PickupRange += (_player.PickupRange * upgrade.percentPickUpRange);

        UpgradeApplied?.Invoke();
    }

}
