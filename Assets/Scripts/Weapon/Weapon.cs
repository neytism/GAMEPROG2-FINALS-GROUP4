using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{

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

    public enum BulletType
    {
        normal,
        exploding
    }
    
}
