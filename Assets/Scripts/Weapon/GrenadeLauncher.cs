using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public static event Action<Weapon> GrenadeLauncherSelected;
    [SerializeField] private Weapon _weaponDefault;

    private Weapon _localWeapon;

    public Weapon LocalWeapon
    {
        get => _localWeapon;
        set => _localWeapon = value;
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
        GrenadeLauncherSelected?.Invoke(_localWeapon);
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
