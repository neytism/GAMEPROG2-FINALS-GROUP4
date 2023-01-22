using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static event Action InteractedWithEnergy;
    public static event Action InteractedWithWeapon;
    
    public static event Action PressedEscape;
    
    [SerializeField] private GameObject _resonateText;
    [SerializeField] private GameObject _weaponText;

    private bool _isNearUpgradeStation = false;
    private bool _isNearWeapon = false;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_isNearUpgradeStation)
            {
                InteractedWithEnergy?.Invoke();
            }
            
            if (_isNearWeapon)
            {
                InteractedWithWeapon?.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedEscape?.Invoke();
            Debug.Log("esc pressed");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PermaUpgradeStation"))
        {
            _isNearUpgradeStation = true;
            _resonateText.SetActive(true);
        } else if (col.gameObject.tag.Equals("WeaponNear"))
        {
            _isNearWeapon = true;
            _weaponText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PermaUpgradeStation"))
        {
            _isNearUpgradeStation = false;
            _resonateText.SetActive(false);
        } else if(col.gameObject.tag.Equals("WeaponNear"))
        {
            _isNearWeapon = false;
            _weaponText.SetActive(false);
        }
    }
}
