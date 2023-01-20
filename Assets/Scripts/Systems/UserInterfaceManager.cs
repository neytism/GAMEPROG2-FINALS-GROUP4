using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    //Level Text
    [SerializeField] private TextMeshProUGUI _levelText;
    
    //Health
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    
    //Permanent Upgrades Panel
    [SerializeField] private GameObject _permaUpgradePanel;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private TextMeshProUGUI _orbCountText;
    
    //counterHUD
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    [SerializeField] private TextMeshProUGUI _orbCountTextHUD;
    private void OnEnable()
    {
        PlayerAddExperience.UpdateExperienceUI += UpdateXPBar;
        PlayerHealth.UpdateHealthUI += UpdateHealthBar;
        PlayerApplyUpgrades.UpgradeApplied += UpdateHealthBar;
        PlayerLevel.UpdateLevelTextUI += UpdateLevelText;
        PlayerInteraction.InteractedWithEnergy += ShowPermaUpgradesPanel;
        UpgradeHolder.ShowUpgradePanel += ShowUpgradePanel;
        PlayerApplyUpgrades.UpgradeApplied += HideUpgradePanel;
        WeaponController.UpdateUI += UpdateAmmoCount;
        PlayerEnergyOrbs.UpdateOrbUI += UpdateOrbCount;
        PermanentUpgradeContainer.DepositedOrbs += UpdateOrbCount;
    }

    private void Start()
    {
        _playerStats = _player.GetComponent<PlayerStats>();
        _weapon = _weaponHolder.SelectedWeaponType.Weapon;
        _XPBar.fillAmount = 0;
        UpdateHealthBar();
        UpdateLevelText(FindObjectOfType<PlayerStats>().Level);
        UpdateAmmoCount();
        UpdateOrbCount();
    }

    private void UpdateXPBar(float xpGain)
    {
        _playerStats.Experience += xpGain;
        _XPBar.fillAmount = _playerStats.Experience / _playerStats.ExperienceRequired;
    }

    private void UpdateLevelText(int level)
    {
        _levelText.text = $"Level: {level}";
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

    private void UpdateAmmoCount()
    {
        _ammoCountText.text = _weapon.currentAmmo.ToString();
    }

    private void UpdateOrbCount()
    {
        _orbCountTextHUD.text = _playerStats.EnergyOrbs.ToString();
        _orbCountText.text = $"{_playerStats.EnergyOrbs} orbs left";
    }

    private void ShowPermaUpgradesPanel()
    {
        Time.timeScale = 0f;
        _permaUpgradePanel.SetActive(true);
    }
    
    public void HidePermaUpgradesPanel()
    {
        Time.timeScale = 1f;
        _permaUpgradePanel.SetActive(false);
    }
    
    
    private void HideUpgradePanel()
    {
        Time.timeScale = 1f;
        _upgradePanel.SetActive(false);
    }

    private void ShowUpgradePanel()
    {
        Time.timeScale = 0f;
        _upgradePanel.SetActive(true);
    }

}
