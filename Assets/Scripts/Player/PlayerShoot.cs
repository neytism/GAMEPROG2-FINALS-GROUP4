using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static event Action shoot;
    public static event Action stopShoot;

    public bool noWeapon;

    private void OnEnable()
    {
        noWeapon = true;
        WeaponHolder.WeaponEquipped += WeaponAcquired;
    }
    private void OnDisable()
    {
        noWeapon = true;
        WeaponHolder.WeaponEquipped -= WeaponAcquired;
    }

    void Update()
    {
        if(noWeapon) return;
        
        if (Input.GetButton("Fire1"))
        {
            shoot?.Invoke();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            stopShoot?.Invoke();
        }
    }

    private void WeaponAcquired()
    {
        noWeapon = false;
    }
}
