using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeHolder : MonoBehaviour
{
    public static event Action ShowUpgradePanel;
    
    [SerializeField] private UpgradeSelectionBox[] _upgradeSelectionBoxes;
    [SerializeField] private Sprite[] _icons;
    [SerializeField] private Upgrades[] _upgrades;
    [SerializeField] private GameObject _holder;

    private void Awake()
    {
        _holder.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerLevel.LevelUp += GenerateUpgrades;
    }
    
    private void OnDisable()
    {
        PlayerLevel.LevelUp -= GenerateUpgrades;
    }

    public void GenerateUpgrades()
    {
        int[] usedIndexes = new int[3];
        int randomIndex;

        
        // Select 3 random upgrades from the available upgrades
        for (int i = 0; i < 3; i++)
        {
            // Generate a random number between 0 and the number of available upgrades
            randomIndex = Random.Range(0, _upgrades.Length);

            // Check if the random number has been used before
            for (int j = 0; j < i; j++)
            {
                if (randomIndex == usedIndexes[j])
                {
                    randomIndex = Random.Range(0,  _upgrades.Length);
                    j = -1;
                }
            }

            // Add the random number to the list of used indexes
            usedIndexes[i] = randomIndex;
            
        }

        for (int i = 0; i < 3; i++)
        {
            _upgradeSelectionBoxes[i]._textBox.text = _upgrades[usedIndexes[i]].upgradeDescription;
            
            //change icon of upgrade
            //_upgradeSelectionBoxes[i]._iconHolder.sprite = _upgrades[usedIndexes[i]]._icon;
            
            _upgradeSelectionBoxes[i].AssignUpgrade( _upgrades[usedIndexes[i]]);
            _upgradeSelectionBoxes[i]._button.GetComponent<PlayerApplyUpgrades>().upgrade = _upgrades[usedIndexes[i]];
        }

        
        ShowUpgradePanel?.Invoke();
    }

    
    
}

[Serializable] public class UpgradeSelectionBox
{
    public Button _button;
    public TextMeshProUGUI _textBox;
    public Image _iconHolder;

    private Upgrades _upgradesAssigned;

    public void AssignUpgrade(Upgrades upgrades)
    {
        _upgradesAssigned = upgrades;
    }
}
