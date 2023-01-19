using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PermanentUpgradeContainer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public PermanentUpgrades[] permanentUpgradesList;
    private PermanentUpgrades permanentUpgrade;
    [SerializeField] private TextMeshProUGUI _upgradeNameText;
    [SerializeField] private TextMeshProUGUI _orbLeftText;
    [SerializeField] private Image _orbDepositedBar;
    private bool _isHolding;

    private List<PermanentUpgrades> _tempList;
    private PermanentUpgrades _tempPermaUp;


    private void Awake()
    {
        CheckUpgrades();
    }

    private void OnEnable()
    {
        UpdateUpgradeNameText();
        UpdateOrbLeftText();
        UpdateOrbFill();
    }

    public void UpdateUpgradeNameText()
    {
        _upgradeNameText.text = permanentUpgrade.upgradeName;
    }

    public void UpdateOrbLeftText()
    {
        _orbLeftText.text = (permanentUpgrade.requiredOrbs - permanentUpgrade.depositedOrbs ).ToString();
    }

    public void UpdateOrbFill()
    {
        _orbDepositedBar.fillAmount = (permanentUpgrade.depositedOrbs / permanentUpgrade.requiredOrbs);
    }

    public void DepositOrb()
    {
        permanentUpgrade.DepositOrbs();

        if (permanentUpgrade.isUnlocked)
        {
            CheckUpgrades();
        }
    }

    public void UpdateDescription()
    {
        GameObject.FindWithTag("PermaUpgradeDescription").GetComponent<TextMeshProUGUI>().text = permanentUpgrade.upgradeDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateDescription();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        DepositOrb();
    }

    public void UpdateUI()
    {
        UpdateOrbLeftText();
        UpdateOrbFill();
        UpdateUpgradeNameText();
    }

    public void CheckUpgrades()
    {
        if (permanentUpgradesList.Length == 1)
        {
            permanentUpgrade = permanentUpgradesList[0];
        }
        else
        {
            _tempList = new List<PermanentUpgrades>();
        
            foreach (PermanentUpgrades perma in permanentUpgradesList)
            {
                if (!perma.isUnlocked)
                {
                    _tempList.Add(perma);
                }
            }

            if (_tempList.Count == 0) return;
        

            _tempPermaUp = _tempList[0];

            foreach (PermanentUpgrades perma in _tempList)
            {
                if (perma.prerequisites.Length < _tempPermaUp.prerequisites.Length)
                {
                    _tempPermaUp = perma;
                }
            }

            permanentUpgrade = _tempPermaUp;
            
            UpdateUI();
        }
    }

}
