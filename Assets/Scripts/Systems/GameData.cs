using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class GameData
{

   //PLAYER
   public bool isFirstTimePlaying;

   //UPGRADES
   
   //Health Reserves
   public int baseHealth;
   public int healthReserveDeposited;
   public int healthReserveLevel;
   
   //Pistol Damage
   public float pistolBaseDamage;
   public int pistolDamageDeposited;
   public int pistolDamageLevel;
   
   
   //for saving after death
   public GameData(bool isFirstTimePlaying, int baseHealth, int healthReserveDeposited, int healthReserveLevel, float pistolBaseDamage, int pistolDamageDeposited, int pistolDamageLevel)
   {
      this.isFirstTimePlaying = isFirstTimePlaying;
      this.baseHealth = baseHealth;
      this.healthReserveDeposited = healthReserveDeposited;
      this.healthReserveLevel = healthReserveLevel;
      this.pistolBaseDamage = pistolBaseDamage;
      this.pistolDamageDeposited = pistolDamageDeposited;
      this.pistolDamageLevel = pistolDamageLevel;
   }


   //for reset 
   public GameData()
   {
      isFirstTimePlaying = true;
      
      baseHealth = 3;
      healthReserveDeposited = 0;
      healthReserveLevel = 1;
      
      pistolBaseDamage = 10;
      pistolDamageDeposited = 0;
      pistolDamageLevel = 1;
      
      //add for new PermanentUpgrade
   }
   
}