using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    public TextMeshPro text;

    private string statsText;

    private PlayerStats stats;

    private Weapon weapon;

    public bool isInvincible;
    private bool isPwedeNa = false;
    private int _health;


    private void OnEnable()
    {
        WeaponHolder.WeaponEquipped += AssignWeapon;
    }
    private void OnDisable()
    {
        WeaponHolder.WeaponEquipped -= AssignWeapon;
    }

    private void AssignWeapon()
    {
        stats = FindObjectOfType<PlayerStats>();
        weapon = WeaponInteract.weapon.GetComponent<WeaponController>().Weapon;
        isPwedeNa = true;
    }

    void Update()
    {
        if (isPwedeNa)
        {
            statsText = $"Level: {stats.Level}\n" +
                        $"XP: {stats.Experience} out of {stats.ExperienceRequired}\n" +
                        $"Energy Orbs: {stats.EnergyOrbs}\n" +
                        $"Max Health: {stats.MaxHealth}\n" +
                        $"Current Health: {stats.CurrentHealth}\n" +
                        $"Position: {stats.GetComponent<Transform>().position.normalized}\n" +
                        $"Movement Speed: {stats.MovementSpeed}\n" +
                        $"Selected Weapon: {weapon.weaponName}\n" +
                        $"Max Ammo: {weapon.maxAmmo}\n" +
                        $"Current Ammo: {weapon.currentAmmo}\n" +
                        $"Damage: {weapon.damage}\n" +
                        $"FireRate: {weapon.fireRate}\n" +
                        $"Reload Speed: {weapon.reloadSpeed}\n" +
                        $"Bullet Speed: {weapon.bulletSpeed}\n" +
                        $"Projectiles: {weapon.projectiles}\n" +
                        $"Spread Angle: {weapon.spreadAngle}\n" +
                        $"Knock Back force: {weapon.knockBackForce}\n" +
                        $"Piercing: {weapon.maxPiercing}";
            text.text = statsText;
        }
        

        if (isInvincible)
        {
            stats.CurrentHealth = 4;
        }
        
    }

    public void InvincibilityOn()
    {
        isInvincible = true;
    }
    
    public void InvincibilityOff()
    {
        isInvincible = false;
    }
    
}

//this script is for debugging

//TODO: Generative map
//TODO: Audio Designing
//TODO: Graphics
//TODO: Gfx particles
//TODO: Enemy will move away if healing, or dead, or leveled up
//TODO: Animation system
