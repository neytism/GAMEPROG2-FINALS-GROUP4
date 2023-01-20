using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyOrbs : MonoBehaviour
{
    public static event Action UpdateOrbUI;
    
    private void OnEnable()
    {
        Orbs.EnergyOrbCollected += AddEnergyOrb;
        PermanentUpgradeContainer.DepositedOrbs += DeductEnergyOrb;
    }

    public void AddEnergyOrb()
    {
        GetComponent<PlayerStats>().EnergyOrbs++;
        UpdateOrbUI?.Invoke();
    }

    private void DeductEnergyOrb()
    {
        GetComponent<PlayerStats>().EnergyOrbs--;
        if (GetComponent<PlayerStats>().EnergyOrbs < 0)
        {
            GetComponent<PlayerStats>().EnergyOrbs = 0;
        }
        UpdateOrbUI?.Invoke();
    }
}
