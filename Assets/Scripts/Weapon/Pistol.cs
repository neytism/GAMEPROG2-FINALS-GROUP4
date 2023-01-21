using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    //to save and load
    public float _baseDamage;
    
    [SerializeField] private Weapon _weaponDefault;

    private Weapon _localWeapon;

    private void Awake()
    {
        _baseDamage = SaveSystem._pistolBaseDamage;
        _weaponDefault.damage = _baseDamage;
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
        _localWeapon = _weaponDefault;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
