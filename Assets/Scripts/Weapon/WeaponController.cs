using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public static event Action WeaponReload;
    public static event Action StartShooting;

    private WeaponHolder _weaponHolder;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _firePoint;
    
    private bool reloading;
    private float fireTimer;

    private void OnEnable()
    {
        PlayerShoot.shoot += Fire;
    }

    public Weapon Weapon => _weapon;
    public bool isReloading => reloading;
    
    public float BulletSpeed => _weapon.bulletSpeed;


    private void Awake()
    {
        _weaponHolder = FindObjectOfType<WeaponHolder>();
        _weapon.currentAmmo = _weapon.maxAmmo;
    }
    
    public void Fire()
    {
        if(reloading) return;
        
        StartShooting?.Invoke();
        
        if(_weapon.currentAmmo <= 0)
        {
            Reload();
            return;
        }
        
        if (Time.time < fireTimer + 1f/_weapon.fireRate) return;

        InstanceBullet();

        _weapon.currentAmmo--;
        fireTimer = Time.time;
        Debug.Log($"{_weapon.name} fired, {_weapon.currentAmmo} bullets left");
        
    }

    public void Reload()
    {
        WeaponReload?.Invoke();
        if(_weapon.currentAmmo == _weapon.maxAmmo) return;
        reloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        //can put here animation for reloading
        yield return new WaitForSeconds(_weapon.reloadSpeed);
        _weapon.currentAmmo = _weapon.maxAmmo;
        reloading = false;
    }

    private void InstanceBullet()
    {
        //used looping for equal angle per bullet if multiple projectiles
        for (int i = 0; i < _weapon.projectiles; i++)
        {
            float angle = i * _weapon.spreadAngle - (_weapon.projectiles - 1) / 2 * _weapon.spreadAngle;
            GameObject bullet = ObjectPool.Instance.GetObject(_weaponHolder.SelectedBulletPrefab((int)_weapon.bulletType), _firePoint.position);
            bullet.SetActive(true);
                
            bullet.GetComponent<Rigidbody2D>().AddForce(RotateSpread(_firePoint.right, angle) * _weapon.bulletSpeed,ForceMode2D.Impulse);
        }
    }
    
    Vector3 RotateSpread(Vector3 firePoint, float angle) //rotates vector3.up for weapon spread
    {
        firePoint.Normalize();

        Vector3 axis = Vector3.Cross(firePoint, Vector3.right);

        // handle case where start is colinear with up
        if (axis == Vector3.zero) axis = Vector3.right;

        return Quaternion.AngleAxis(angle, axis) * firePoint;
    }

   
    
}

