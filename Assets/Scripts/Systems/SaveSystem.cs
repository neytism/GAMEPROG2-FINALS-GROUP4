using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    #region Instance

    private static SaveSystem _instance;
    public static  SaveSystem Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        LoadData();
    }

    #endregion

    private GameData _newGameData;
    private GameData _localGameData;
    
    private const string SAVE_DATA_KEY = "PlayerData";
    
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
    
    private void OnEnable()
    {
        PlayerHealth.PlayerDeath += SaveGame;
        UserInterfaceManager.SaveGamePlsHuHu += SaveGame;
    }
    
    private void OnDisable()
    {
        PlayerHealth.PlayerDeath -= SaveGame;
        UserInterfaceManager.SaveGamePlsHuHu -= SaveGame;
    }

    //called on awake
    public void LoadData()
    {
        Debug.Log("Checking if data available");
        
        if (PlayerPrefs.HasKey(SAVE_DATA_KEY))
        {
            Debug.Log("Has data to load");
            var jsonToConvert = PlayerPrefs.GetString(SAVE_DATA_KEY);
            Debug.Log($"{nameof(SaveSystem)}.LoadData : {jsonToConvert}");
            _localGameData = JsonConvert.DeserializeObject<GameData>(jsonToConvert, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            
            // Setting up data when loading

            LoadDataOnLocalData(_localGameData);
            
        }
        else
        {
            Debug.Log("No available data to load, new game only");
            
            _newGameData = new GameData();
            var data = JsonConvert.SerializeObject(_newGameData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            PlayerPrefs.SetString(SAVE_DATA_KEY, data);
            LoadDataOnLocalData(_newGameData);
            //AchievementManager.InitializeData();
            Debug.Log($"{nameof(SaveSystem)}.LoadData : {data}");
        } 
    }
    
    private void LoadDataOnLocalData(GameData gameData)
    {
    //PLAYER
    isFirstTimePlaying = gameData.isFirstTimePlaying;

    //UPGRADES
   
    //Health Reserves
    baseHealth = gameData.baseHealth;
    healthReserveDeposited = gameData.healthReserveDeposited;
    healthReserveLevel = gameData.healthReserveLevel;
    healthReserveRequired = gameData.healthReserveRequired;

    //Pistol Damage
    pistolAdditionalDamage = gameData.pistolAdditionalDamage;
    pistolDamageDeposited = gameData.pistolDamageDeposited;
    pistolDamageLevel = gameData.pistolDamageLevel;
    pistolDamageRequired = gameData.pistolDamageRequired;
   
    //Shotgun Damage
    shotgunAdditionalDamage = gameData.shotgunAdditionalDamage;
    shotgunDamageDeposited = gameData.shotgunDamageDeposited;
    shotgunDamageLevel = gameData.shotgunDamageLevel;
    shotgunDamageRequired = gameData.shotgunDamageRequired;
   
    //Grenade Launcher Damage
    grenadeLauncherAdditionalDamage = gameData.grenadeLauncherAdditionalDamage;
    grenadeLauncherDamageDeposited = gameData.grenadeLauncherDamageDeposited;
    grenadeLauncherDamageLevel = gameData.grenadeLauncherDamageLevel;
    grenadeLauncherDamageRequired = gameData.grenadeLauncherDamageRequired;
    
    }
    
    public void SaveGame()
    {
        //save data
        Debug.Log($"PISTOL BASE DAMAGE : {pistolAdditionalDamage}");
        _localGameData = new GameData(isFirstTimePlaying,baseHealth,healthReserveDeposited,healthReserveLevel,healthReserveRequired,pistolAdditionalDamage,pistolDamageDeposited,pistolDamageLevel,pistolDamageRequired,shotgunAdditionalDamage,shotgunDamageDeposited,shotgunDamageLevel,shotgunDamageRequired,grenadeLauncherAdditionalDamage,grenadeLauncherDamageDeposited,grenadeLauncherDamageLevel,grenadeLauncherDamageRequired);
        var playerData = JsonConvert.SerializeObject(_localGameData, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        Debug.Log($"{nameof(SaveSystem)}.{nameof(SaveGame)} : {playerData}");
        PlayerPrefs.SetString(SAVE_DATA_KEY, playerData);
    }
    
    public void ResetGameData()  // RESETS ALL DATA
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("GAME HAS BEEN RESET");
        SceneManager.LoadScene("MainMenu");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}