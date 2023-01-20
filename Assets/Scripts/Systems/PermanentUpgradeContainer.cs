using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PermanentUpgradeContainer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public static event Action DepositedOrbs;
    
    public PermanentUpgrades[] permanentUpgradesList;
    private PermanentUpgrades permanentUpgrade;
    [SerializeField] private TextMeshProUGUI _upgradeNameText;
    [SerializeField] private TextMeshProUGUI _orbLeftText;
    [SerializeField] private TextMeshProUGUI _orbTextCount;
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
        if (FindObjectOfType<PlayerStats>().EnergyOrbs == 0) return;
        permanentUpgrade.DepositOrbs();
        
        if (permanentUpgrade.isUnlocked)
        {
            CheckUpgrades();
        }
        else
        {
            DepositedOrbs?.Invoke();
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

    public void UpdateUIAfterUnlock()
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
            //creates new list
            _tempList = new List<PermanentUpgrades>();
        
            //adds to new list if upgrade is locked
            foreach (PermanentUpgrades perma in permanentUpgradesList)
            {
                if (!perma.isUnlocked)
                {
                    _tempList.Add(perma);
                }
            }

            
            if (_tempList.Count == 0)
            {
                //will get the highest upgrade level if there is no locked Upgrade found 
                _tempPermaUp = permanentUpgradesList[0];
                
                foreach (PermanentUpgrades perma in permanentUpgradesList)
                {
                    if (perma.levelOfUpgrade > _tempPermaUp.levelOfUpgrade)
                    {
                        _tempPermaUp = perma;
                    }
                }
            }
            else
            {
                //gets the lowest locked level
                _tempPermaUp = _tempList[0];
                foreach (PermanentUpgrades perma in _tempList)
                {
                    if (perma.levelOfUpgrade < _tempPermaUp.levelOfUpgrade)
                    {
                        _tempPermaUp = perma;
                    }
                }
            }

            permanentUpgrade = _tempPermaUp;
            
            UpdateUIAfterUnlock();
        }
    }

}
