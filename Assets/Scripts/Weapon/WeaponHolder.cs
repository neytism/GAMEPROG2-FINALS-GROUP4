using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public static event Action WeaponEquipped;
    
    [SerializeField] private GameObject[] _bulletPrefabs;
    private static GameObject _selectedWeaponPrefab;
    private static WeaponController _selectedWeaponType;
    public GameObject bulletObject;
    private GameObject _selected;
    private int _temp;
    private Transform _tempTransform;

    public static Weapon selectedWeapon;

    public static GameObject SelectedWeaponPrefab => _selectedWeaponPrefab;
    public WeaponController SelectedWeaponType => _selectedWeaponType;


    private void OnEnable()
    {
        _tempTransform = gameObject.transform;
        WeaponInteract.WeaponCollected += SummonWeapon;
    }
    
    private void OnDisable()
    {
        WeaponInteract.WeaponCollected -= SummonWeapon;
    }

    private void SummonWeapon(GameObject _selected)
    {
        _selected = WeaponInteract.weapon;
        _selectedWeaponPrefab = Instantiate(_selected,_tempTransform);
        _selectedWeaponType = _selected.GetComponent<WeaponController>();
        _temp = _selected.GetComponent<WeaponController>().selectedBulletType;
        bulletObject = _bulletPrefabs[_temp];
        selectedWeapon = _selectedWeaponType.Weapon;
        WeaponEquipped?.Invoke();
    }

    public enum WeaponType
    {
        Pistol,
        ShotGun,
        GrenadeLauncher
        //add here new weapon type
    }
    
}

//this script hold all weapon and bullet types for referencing