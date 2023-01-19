using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyOrbs : MonoBehaviour
{
    private void OnEnable()
    {
        Orbs.EnergyOrbCollected += AddEnergyOrb;
    }

    private void AddEnergyOrb()
    {
        GetComponent<PlayerStats>().EnergyOrbs++;
    }

    private void DeductEnergyOrb()
    {
        GetComponent<PlayerStats>().EnergyOrbs--;
    }
}
