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
    
    [SerializeField] private GameObject[] _bulletPrefab;
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
        //You can use a coroutine to make reload animation
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(_weapon.reloadSpeed);
        Debug.Log("Reload Complete");
        _weapon.currentAmmo = _weapon.maxAmmo;
        reloading = false;
    }

    private void InstanceBullet()
    {
            for (int i = 0; i < _weapon.projectiles; i++)
            {
                float angle = i * _weapon.spreadAngle - (_weapon.projectiles - 1) / 2 * _weapon.spreadAngle;
                GameObject bullet = ObjectPool.Instance.GetObject(_bulletPrefab[(int)_weapon.bulletType], _firePoint.position);
                bullet.SetActive(true);
                
                bullet.GetComponent<Rigidbody2D>().AddForce(RotateSpread(_firePoint.right, angle) * _weapon.bulletSpeed,ForceMode2D.Impulse);
            }
    }
    
    Vector3 RotateSpread(Vector3 start, float angle) //rotates vector3.up for weapon spread
    {
        // if you know start will always be normalized, can skip this step
        start.Normalize();

        Vector3 axis = Vector3.Cross(start, Vector3.right);

        // handle case where start is colinear with up
        if (axis == Vector3.zero) axis = Vector3.right;

        return Quaternion.AngleAxis(angle, axis) * start;
    }

   
    
}

