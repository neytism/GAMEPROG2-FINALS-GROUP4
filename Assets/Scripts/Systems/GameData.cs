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
   public int healthReserveRequired;

   //Pistol Damage
   public float pistolAdditionalDamage;
   public int pistolDamageDeposited;
   public int pistolDamageLevel;
   public int pistolDamageRequired;
   
   //Shotgun Damage
   public float shotgunAdditionalDamage;
   public int shotgunDamageDeposited;
   public int shotgunDamageLevel;
   public int shotgunDamageRequired;
   
   //Grenade Launcher Damage
   public float grenadeLauncherAdditionalDamage;
   public int grenadeLauncherDamageDeposited;
   public int grenadeLauncherDamageLevel;
   public int grenadeLauncherDamageRequired;

   //for saving after death
   public GameData(bool isFirstTimePlaying, int baseHealth, int healthReserveDeposited, int healthReserveLevel, int healthReserveRequired, float pistolAdditionalDamage, int pistolDamageDeposited, int pistolDamageLevel, int pistolDamageRequired, float shotgunAdditionalDamage, int shotgunDamageDeposited, int shotgunDamageLevel, int shotgunDamageRequired, float grenadeLauncherAdditionalDamage, int grenadeLauncherDamageDeposited, int grenadeLauncherDamageLevel, int grenadeLauncherDamageRequired)
   {
      this.isFirstTimePlaying = isFirstTimePlaying;
      this.baseHealth = baseHealth;
      this.healthReserveDeposited = healthReserveDeposited;
      this.healthReserveLevel = healthReserveLevel;
      this.healthReserveRequired = healthReserveRequired;
      this.pistolAdditionalDamage = pistolAdditionalDamage;
      this.pistolDamageDeposited = pistolDamageDeposited;
      this.pistolDamageLevel = pistolDamageLevel;
      this.pistolDamageRequired = pistolDamageRequired;
      this.shotgunAdditionalDamage = shotgunAdditionalDamage;
      this.shotgunDamageDeposited = shotgunDamageDeposited;
      this.shotgunDamageLevel = shotgunDamageLevel;
      this.shotgunDamageRequired = shotgunDamageRequired;
      this.grenadeLauncherAdditionalDamage = grenadeLauncherAdditionalDamage;
      this.grenadeLauncherDamageDeposited = grenadeLauncherDamageDeposited;
      this.grenadeLauncherDamageLevel = grenadeLauncherDamageLevel;
      this.grenadeLauncherDamageRequired = grenadeLauncherDamageRequired;
   }

   //for reset 
   public GameData()
   {
      isFirstTimePlaying = true;
      
      baseHealth = 3;
      healthReserveDeposited = 0;
      healthReserveLevel = 1;
      healthReserveRequired = 10;
      
      pistolAdditionalDamage = 0;
      pistolDamageDeposited = 0;
      pistolDamageLevel = 1;
      pistolDamageRequired = 10;
      
      shotgunAdditionalDamage = 0;
      shotgunDamageDeposited = 0;
      shotgunDamageLevel = 1;
      shotgunDamageRequired = 10;
      
      grenadeLauncherAdditionalDamage = 0;
      grenadeLauncherDamageDeposited = 0;
      grenadeLauncherDamageLevel = 1;
      grenadeLauncherDamageRequired = 10;

      //add for new PermanentUpgrade
   }
   
}