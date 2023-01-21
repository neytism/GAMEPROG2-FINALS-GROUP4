using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static event Action UpdateHealthUI;
    public static event Action PlayerDeath;
    
    private PlayerStats _playerStats;
    private PlayerHurt _playerHurt;

    private float _holdTime;

    private void OnEnable()
    {
        UpdateHealthUI += CheckHealth;
        PlayerHurt.ReduceHealth += ReduceHealth;
    }
    
    private void OnDisable()
    {
        UpdateHealthUI -= CheckHealth;
        PlayerHurt.ReduceHealth -= ReduceHealth;
    }

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerHurt = GetComponent<PlayerHurt>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _holdTime += Time.deltaTime;
            if (_holdTime >= 2)
            {
                AddHealth(1);
                _holdTime = 0;
            }
        }
    }

    private void CheckHealth()
    {
        if (_playerStats.CurrentHealth >= _playerStats.MaxHealth)
        {
            _playerStats.CurrentHealth = _playerStats.MaxHealth;
        }

        if (_playerStats.MaxHealth >= 30)
        {
            _playerStats.MaxHealth = 30;
        }
    }

    private void ReduceHealth()
    {
        if (!_playerHurt.IsKnockBack)
        {
            _playerStats.CurrentHealth--;
            UpdateHealthUI?.Invoke();
            if (_playerStats.CurrentHealth == 0) //if player is dead
            {
                PlayerDeath?.Invoke();
            }
        }
    }

    private void AddHealth(int amount)
    {
        _playerStats.CurrentHealth += amount;
        UpdateHealthUI?.Invoke();
    }
    
    
}
