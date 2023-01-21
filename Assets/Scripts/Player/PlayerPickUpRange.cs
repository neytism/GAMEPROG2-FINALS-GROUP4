using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpRange : MonoBehaviour
{
    private CircleCollider2D _collider;

    private void OnEnable()
    {
        PlayerApplyUpgrades.UpgradeApplied += UpdateRadius;
    }
    
    private void OnDisable()
    {
        PlayerApplyUpgrades.UpgradeApplied -= UpdateRadius;
    }

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
       UpdateRadius();
    }

    private void UpdateRadius()
    {
        _collider.radius = FindObjectOfType<PlayerStats>().PickupRange;
    }
}
