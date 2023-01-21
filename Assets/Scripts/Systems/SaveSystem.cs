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
    public static bool _isFirstTimePlaying;

    //UPGRADES
   
    //Health Reserves
    public static int _baseHealth;
    public static int _healthReserveDeposited;
    public static int _healthReserveLevel;
   
    //Pistol Damage
    public static float _pistolBaseDamage;
    public static int _pistolDamageDeposited;
    public static int _pistolDamageLevel;
    
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
    _isFirstTimePlaying = gameData.isFirstTimePlaying;

    //UPGRADES
   
    //Health Reserves
    _baseHealth = gameData.baseHealth;
    _healthReserveDeposited = gameData.healthReserveDeposited;
    _healthReserveLevel = gameData.healthReserveLevel;
   
    //Pistol Damage
    _pistolBaseDamage = gameData.pistolBaseDamage;
    _pistolDamageDeposited = gameData.healthReserveDeposited;
    _pistolDamageLevel = gameData.pistolDamageLevel;
    }
    
    public void SaveGame()
    {
        //save data
        _localGameData = new GameData(_isFirstTimePlaying, _baseHealth,_healthReserveDeposited, _healthReserveLevel, _pistolBaseDamage, _pistolDamageDeposited, _pistolDamageLevel);
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