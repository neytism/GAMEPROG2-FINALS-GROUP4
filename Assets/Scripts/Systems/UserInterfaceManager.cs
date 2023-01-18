using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private WeaponHolder _weaponHolder;
    private PlayerStats _playerStats;
    private Weapon _weapon;

    //XP BAR
    [SerializeField] private Image _XPBar;
    private float temp;
    
    //Health
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;

    private void OnEnable()
    {
        PlayerAddExperience.UpdateExperienceUI += UpdateXPBar;
        PlayerHealth.UpdateHealthUI += UpdateHealthBar;
        PlayerApplyUpgrades.UpgradeApplied += UpdateHealthBar;
    }

    private void Start()
    {
        _playerStats = _player.GetComponent<PlayerStats>();
        _weapon = _weaponHolder.SelectedWeaponType.Weapon;
        _XPBar.fillAmount = 0;
        UpdateHealthBar();
    }

    private void UpdateXPBar(float xpGain)
    {
        _playerStats.Experience += xpGain;
        _XPBar.fillAmount = _playerStats.Experience / _playerStats.ExperienceRequired;
    }

    private void UpdateHealthBar()
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < _playerStats.CurrentHealth)
            {
                _hearts[i].sprite = _fullHeart;
            }
            else
            {
                _hearts[i].sprite = _emptyHeart;
            }

            if (i < _playerStats.MaxHealth)
            {
                _hearts[i].enabled = true;
            }
            else
            {
                _hearts[i].enabled = false;
            }
        }
    }

}
