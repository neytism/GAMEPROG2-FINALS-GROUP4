using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShotgunDamage : MonoBehaviour,IPointerEnterHandler
{
    public static event Action DepositedOrbs;
    
    ///must be saved & loaded
    private int _levelOfUpgrade = 1;
    private float _depositedOrbs;
    private float _requiredOrbs;
    
    //reset on start
    [SerializeField] private TextMeshProUGUI _upgradeNameText;
    [SerializeField] private TextMeshProUGUI _requireOrbTextCount;
    [SerializeField] private Image _orbDepositedBar;
    [SerializeField] private int _addedvValue;
    [SerializeField] private float _multiplier;
    
    [SerializeField] private GameObject shotGun;
    
    //description
    public Sprite _icon;
    public string upgradeName;
    [TextArea] public string upgradeDescription;
    public Image iconEditor;


    private void OnEnable()
    {
        LoadUpgradeData();
        
        UpdateUpgradeNameText();
        UpdateOrbLeftText();
        UpdateOrbFill();
        UpdateUpgradeIcon();
    }
    
    public void UpdateUpgradeIcon()
    {
        iconEditor.sprite = _icon;
    }
    
    public void UpdateUpgradeNameText()
    {
        _upgradeNameText.text = $"{upgradeName} {_levelOfUpgrade}";
    }
    

    public void UpdateOrbLeftText()
    {
        _requireOrbTextCount.text = (_requiredOrbs - _depositedOrbs ).ToString();
    }

    public void UpdateOrbFill()
    {
        _orbDepositedBar.fillAmount = (_depositedOrbs / _requiredOrbs);
    }
    
    public void UpdateDescription()
    {
        GameObject.FindWithTag("PermaUpgradeDescription").GetComponent<TextMeshProUGUI>().text = upgradeDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateDescription();
    }
    
    
    public void DepositOrb()
    {
        if (FindObjectOfType<PlayerStats>().EnergyOrbs == 0) return;
        _depositedOrbs++;
        
        CheckUpgrades();
        UpdateOrbFill();
        UpdateOrbLeftText();
        DepositedOrbs?.Invoke();
        
        //SAVING PER UPGRADE
        SaveSystem.Instance.shotgunDamageDeposited = (int)_depositedOrbs;
        SaveSystem.Instance.shotgunDamageLevel = _levelOfUpgrade;
        SaveSystem.Instance.shotgunDamageRequired = (int)_requiredOrbs;
        SaveSystem.Instance.shotgunAdditionalDamage = shotGun.GetComponent<Shotgun>()._additionalDamageUpgrade;

    }
    
    public void CheckUpgrades()
    {
        if (_depositedOrbs >= _requiredOrbs)
        {
            //WeaponUpgrade
            ApplyUpgrade();
            
            _levelOfUpgrade++;
            _requiredOrbs = (int)( _requiredOrbs + (_levelOfUpgrade* _multiplier));
            _depositedOrbs = 0;
            
            UpdateUpgradeNameText();
            UpdateOrbFill();
            UpdateOrbLeftText();
        }
    }
    
    private void LoadUpgradeData()
    {
        _levelOfUpgrade = SaveSystem.Instance.grenadeLauncherDamageLevel;
        _depositedOrbs = SaveSystem.Instance.grenadeLauncherDamageDeposited;
        _requiredOrbs = SaveSystem.Instance.grenadeLauncherDamageRequired;
    }
    
    private void ApplyUpgrade()
    {
        shotGun.GetComponent<Shotgun>()._additionalDamageUpgrade += _addedvValue;
        shotGun.GetComponent<Shotgun>().localWeapon.damage += _addedvValue;
    }
}
