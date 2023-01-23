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
    public static event Action UpdateUI;

    private WeaponHolder _weaponHolder;
    public Weapon _weapon;
    [SerializeField] private Transform _firePoint;

    public int selectedBulletType;
    
    private bool reloading;
    private float fireTimer;
    
    private float _timeSinceLastFire;
    private float _autoReloadTime = 1.5f;
    

    private void OnEnable()
    {
        PlayerShoot.shoot += Fire;
        _weaponHolder = FindObjectOfType<WeaponHolder>();
        _weapon.currentAmmo = _weapon.maxAmmo;
        selectedBulletType = (int)_weapon.bulletType;
    }
    private void OnDisable()
    {
        PlayerShoot.shoot -= Fire;
    }

    private void Update()
    {
        //auto reload after _autoReloadTime seconds
        _timeSinceLastFire += Time.deltaTime;
        if (_timeSinceLastFire >= _autoReloadTime && _weapon.currentAmmo < _weapon.maxAmmo && !reloading)
        {
            Reload();
            _timeSinceLastFire = 0;
        }
    }

    public Weapon Weapon => _weapon;
    public bool isReloading => reloading;
    
    public float BulletSpeed => _weapon.bulletSpeed;

    public void Fire()
    {
        //will not fire if reloading
        if(reloading) return;
        StartShooting?.Invoke();
        
        UpdateUI?.Invoke();
        
        if(_weapon.currentAmmo <= 0)
        {
            Reload();
            return;
        }
        
        if (Time.time < fireTimer + 1f/_weapon.fireRate) return;

        SoundManager.Instance.PlayOnce(SoundManager.Sounds.WeaponFire);
        InstanceBullet();
        _timeSinceLastFire = 0;
        

        _weapon.currentAmmo--;
        
        fireTimer = Time.time;
        
    }

    public void Reload()
    {
        WeaponReload?.Invoke();
        if(_weapon.currentAmmo == _weapon.maxAmmo) return;
        reloading = true;
        SoundManager.Instance.PlayOnce(SoundManager.Sounds.ReloadStart);
        StartCoroutine(ReloadCoroutine());
    }
    
    private IEnumerator ReloadCoroutine()
    {
        //animation for reloading

        int ammoToReload = _weapon.maxAmmo - _weapon.currentAmmo;
        float reloadInterval = _weapon.reloadSpeed / ammoToReload;

        for (int i = 0; i < ammoToReload; i++)
        {
            yield return new WaitForSeconds(reloadInterval);
            SoundManager.Instance.PlayOnce(SoundManager.Sounds.ReloadContinue);
            _weapon.currentAmmo++;
            UpdateUI?.Invoke();
        }

        reloading = false;
    }

    private void InstanceBullet()
    {
        //used looping for equal angle per bullet if multiple projectiles
        for (int i = 0; i < _weapon.projectiles; i++)
        {
            float angle = i * _weapon.spreadAngle - (_weapon.projectiles - 1) / 2 * _weapon.spreadAngle;
            GameObject bullet = ObjectPool.Instance.GetObject(_weaponHolder.bulletObject, _firePoint.position);
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

