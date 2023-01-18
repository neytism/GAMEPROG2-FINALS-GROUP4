using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _movementSpeed;

    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private float _pickupRange;

    private float _experience;
    private float _experienceRequired;

    
    private float _level;
    
    #region GetSet

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    public float MovementSpeed
    {
        get => _movementSpeed;
        set => _movementSpeed = value;
    }
    
    public float WalkingSpeed
    {
        get => _movementSpeed/2;
    }
    
    public float RunningSpeed
    {
        get => _movementSpeed;
    }

    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set => _currentWeapon = value;
    }

    public float PickupRange
    {
        get => _pickupRange;
        set => _pickupRange = value;
    }

    public float Experience
    {
        get => _experience;
        set => _experience = value;
    }
    
    public float ExperienceRequired
    {
        get => _experienceRequired;
        set => _experienceRequired = value;
    }

    public float Level
    {
        get => _level;
        set => _level = value;
    }

    #endregion
    
}
