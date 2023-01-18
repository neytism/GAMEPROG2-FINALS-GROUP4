using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    [SerializeField] private Image _reloadBar;
    
    private float _reloadTime;
    
    private WeaponController _weapon;

    private bool _playerNoAmmo;
    private void OnEnable()
    {
        WeaponController.WeaponReload += ReloadBarStart;
    }

    private void Start()
    {
        _playerNoAmmo = false;
        _reloadBar.enabled = false;
        _weapon = WeaponHolder.SelectedWeaponPrefab.GetComponent<WeaponController>();
    }

    void Update()
    {
        if (_playerNoAmmo)
        {
            if (_weapon.isReloading)
            {
                if (_reloadTime <= _weapon.Weapon.reloadSpeed)
                {
                    _reloadBar.enabled = true;
                    _reloadTime += Time.deltaTime;
                    _reloadBar.fillAmount = _reloadTime  /  _weapon.Weapon.reloadSpeed;
                }
            }
            else
            {
                _reloadBar.enabled = false;
                _reloadTime = 0;
                _playerNoAmmo = false;
            }
        }
        
    }
    
    private void ReloadBarStart()
    {
        _playerNoAmmo = true;
    }
}
