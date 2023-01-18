using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponPrefabs;
    [SerializeField] private GameObject[] _bulletPrefabs;
    [SerializeField] private WeaponType _weaponType;
    private static GameObject _selectedWeaponPrefab;
    private static WeaponController _selectedWeaponType;
    

    public static GameObject SelectedWeaponPrefab => _selectedWeaponPrefab;
    public WeaponController SelectedWeaponType => _selectedWeaponType;

    public GameObject SelectedBulletPrefab(int i) => _bulletPrefabs[i];



    private void Awake()
    {
        _selectedWeaponPrefab = Instantiate(_weaponPrefabs[(int)_weaponType],gameObject.transform);
        _selectedWeaponType = _selectedWeaponPrefab.GetComponent<WeaponController>();
    }
    
    public enum WeaponType
    {
        Pistol,
        GrenadeLauncher
        //add here new weapon type
    }
}

//this script hold all weapon and bullet types for referencing