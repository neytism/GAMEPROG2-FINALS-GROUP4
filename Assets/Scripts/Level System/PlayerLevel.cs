using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{

    public static event Action LevelUp;
    public static event Action<int> UpdateLevelTextUI;
    
    private PlayerStats _playerStats;
    private LevelScaling _levelScaling;


    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
        _levelScaling = GetComponent<LevelScaling>();
        IncreaseLevel();
    }

    private void OnEnable()
    {
        PlayerAddExperience.IncreasePlayerLevel += IncreaseLevel;
    }

    public void IncreaseLevel()
    {
        _playerStats.Level++;
        LevelUp?.Invoke();
        UpdateLevelTextUI?.Invoke(_playerStats.Level);
        _playerStats.ExperienceRequired = _levelScaling.CalculateRequiredExp(_playerStats.Level);
    }

}