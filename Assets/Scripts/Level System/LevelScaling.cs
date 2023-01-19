using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScaling : MonoBehaviour
{
    public PlayerStats _playerStats;

    // These multiplier are made public to modify the scaling whenever you want
    [Header("Multiplier")]
    [Range(0f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(5f, 14f)]
    public float divisionMultiplier = 7;


    public int CalculateRequiredExp()
    {
        int solveForRequiredExp = 0;
        for (int levelCycle = 1; levelCycle <= _playerStats.Level; levelCycle++)
        {
            solveForRequiredExp += (int)MathF.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredExp / 4;
    }
}
