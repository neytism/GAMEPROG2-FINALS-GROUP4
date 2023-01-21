using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthReserves : MonoBehaviour, IPointerEnterHandler
{
    //must be saved & loaded
    private int _levelOfUpgrade = 1;
    public float depositedOrbs;
    
    //reset on start
    
    public static event Action DepositedOrbs;
    
    [SerializeField] private TextMeshProUGUI _upgradeNameText;
    [SerializeField] private TextMeshProUGUI _orbLeftText;
    [SerializeField] private TextMeshProUGUI _requireOrbTextCount;
    [SerializeField] private Image _orbDepositedBar;
    [SerializeField] private int _addedvValue;
    [SerializeField] private float _multiplier;
    
    
    
    //description
    public Sprite _icon;
    public string upgradeName;
    [TextArea] public string upgradeDescription;
    
    //data
    public float requiredOrbs;
    
    
    private void OnEnable()
    {
        UpdateUpgradeNameText();
        UpdateOrbLeftText();
        UpdateOrbFill();
    }
    
    public void UpdateUpgradeNameText()
    {
        _upgradeNameText.text = $"{upgradeName} {_levelOfUpgrade}";
    }
    

    public void UpdateOrbLeftText()
    {
        _requireOrbTextCount.text = (requiredOrbs - depositedOrbs ).ToString();
    }

    public void UpdateOrbFill()
    {
        _orbDepositedBar.fillAmount = (depositedOrbs / requiredOrbs);
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
        depositedOrbs++;
        
        CheckUpgrades();
        UpdateOrbFill();
        UpdateOrbLeftText();
        DepositedOrbs?.Invoke();
        SaveSystem._healthReserveDeposited = (int)depositedOrbs;
        SaveSystem._healthReserveLevel = _levelOfUpgrade;
        SaveSystem._baseHealth = FindObjectOfType<PlayerStats>().BaseMaxHealth;
    }
    
    public void CheckUpgrades()
    {
        if (depositedOrbs >= requiredOrbs)
        {
            FindObjectOfType<PlayerStats>().BaseMaxHealth += _addedvValue;
            FindObjectOfType<PlayerStats>().MaxHealth += _addedvValue;
            FindObjectOfType<PlayerStats>().CurrentHealth += _addedvValue;
            _levelOfUpgrade++;
            requiredOrbs = (int)( requiredOrbs * (_levelOfUpgrade* _multiplier));
            depositedOrbs = 0;
            UpdateUpgradeNameText();
            UpdateOrbFill();
            UpdateOrbLeftText();
        }
    }
}
