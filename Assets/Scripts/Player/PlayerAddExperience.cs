using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddExperience : MonoBehaviour
{
    public static event Action<float> UpdateExperienceUI;
    public static event Action IncreasePlayerLevel;

    private PlayerStats _playerStats;
    
    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerStats.ExperienceRequired = 10; //remove if there is existing leveling system
    }
    private void OnEnable()
    {
        Orbs.ExperienceCollected += AddExperience;
    }
    
    private void OnDisable()
    {
        Orbs.ExperienceCollected -= AddExperience;
    }
    private void AddExperience(float experience)
    {
        UpdateExperienceUI?.Invoke(experience);
        Debug.Log($"XP: {_playerStats.Experience} / {_playerStats.ExperienceRequired}");
        if (_playerStats.Experience >= _playerStats.ExperienceRequired)
        {
            _playerStats.Experience = 0;
            IncreasePlayerLevel?.Invoke();
        }
    }
}
