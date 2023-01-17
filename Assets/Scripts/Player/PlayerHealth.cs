using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static event Action UpdateHealthUI;
    private PlayerStats _playerStats;

    private void OnEnable()
    {
        UpdateHealthUI += CheckHealth;
    }

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _playerStats.CurrentHealth--;
            UpdateHealthUI?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _playerStats.CurrentHealth++;
            UpdateHealthUI?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            _playerStats.MaxHealth--;
            UpdateHealthUI?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            _playerStats.MaxHealth++;
            _playerStats.CurrentHealth++;
            UpdateHealthUI?.Invoke();
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
}
