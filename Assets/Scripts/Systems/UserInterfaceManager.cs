using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    public static event Action SaveGamePlsHuHu; 
    public static event Action KillSelf;

    public static event Action ResetWeaponToDefault; 

    [SerializeField] private GameObject _player;
    [SerializeField] private WeaponHolder _weaponHolder;
    private PlayerStats _playerStats;
    private Weapon _weapon;

    //XP BAR
    [SerializeField] private Image _XPBar;
    private float temp;
    
    //Level Text
    [SerializeField] private TextMeshProUGUI _levelText;
    
    //Health
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    
    //Permanent Upgrades Panel
    [SerializeField] private GameObject _permaUpgradePanel;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private TextMeshProUGUI _orbCountText;
    
    //counterHUD
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    [SerializeField] private TextMeshProUGUI _orbCountTextHUD;
    
    //Game Over Panel
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _lostOrbText;
    
    //Pause Menu
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _areYouSureQuitPanel;
    [SerializeField] private GameObject _areYouSureRestartPanel;
    private bool _isPaused = false;
    private void OnEnable()
    {
        WeaponHolder.WeaponEquipped += AssignWeapon;
        PlayerAddExperience.UpdateExperienceUI += UpdateXPBar;
        PlayerHealth.UpdateHealthUI += UpdateHealthBar;
        PlayerApplyUpgrades.UpgradeApplied += UpdateHealthBar;
        PlayerLevel.UpdateLevelTextUI += UpdateLevelText;
        PlayerInteraction.InteractedWithEnergy += ShowPermaUpgradesPanel;
        UpgradeHolder.ShowUpgradePanel += ShowUpgradePanel;
        PlayerApplyUpgrades.UpgradeApplied += HideUpgradePanel;
        WeaponController.UpdateUI += UpdateAmmoCount;
        PlayerEnergyOrbs.UpdateOrbUI += UpdateOrbCount;
        HealthReserves.DepositedOrbs += UpdateOrbCount;
        PistolDamage.DepositedOrbs += UpdateOrbCount;
        ShotgunDamage.DepositedOrbs += UpdateOrbCount;
        GrenadeLauncherDamage.DepositedOrbs += UpdateOrbCount;
        PlayerHealth.PlayerDeath += ShowGameOverPanel;
        PlayerInteraction.PressedEscape += PauseChecker;
    }
    private void OnDisable()
    {
        WeaponHolder.WeaponEquipped -= AssignWeapon;
        PlayerAddExperience.UpdateExperienceUI -= UpdateXPBar;
        PlayerHealth.UpdateHealthUI -= UpdateHealthBar;
        PlayerApplyUpgrades.UpgradeApplied -= UpdateHealthBar;
        PlayerLevel.UpdateLevelTextUI -= UpdateLevelText;
        PlayerInteraction.InteractedWithEnergy -= ShowPermaUpgradesPanel;
        UpgradeHolder.ShowUpgradePanel -= ShowUpgradePanel;
        PlayerApplyUpgrades.UpgradeApplied -= HideUpgradePanel;
        WeaponController.UpdateUI -= UpdateAmmoCount;
        PlayerEnergyOrbs.UpdateOrbUI -= UpdateOrbCount;
        HealthReserves.DepositedOrbs -= UpdateOrbCount;
        PistolDamage.DepositedOrbs -= UpdateOrbCount;
        ShotgunDamage.DepositedOrbs -= UpdateOrbCount;
        GrenadeLauncherDamage.DepositedOrbs -= UpdateOrbCount;
        PlayerHealth.PlayerDeath -= ShowGameOverPanel;
        PlayerInteraction.PressedEscape -= PauseChecker;
    }

    private void Start()
    {
        SoundManager.Instance.PlayLoop(SoundManager.Sounds.GameBGM);
        SoundManager.Instance.PlayFadeIn(SoundManager.Sounds.GameBGM,0.005f, .5f);
        _playerStats = _player.GetComponent<PlayerStats>();
        _XPBar.fillAmount = 0;
        UpdateHealthBar();
        UpdateLevelText(FindObjectOfType<PlayerStats>().Level);
        UpdateOrbCount();
    }

    private void AssignWeapon()
    {
        _weapon = _weaponHolder.SelectedWeaponType.Weapon;
        UpdateAmmoCount();
    }

    private void UpdateXPBar(float xpGain)
    {
        _playerStats.Experience += xpGain;
        _XPBar.fillAmount = _playerStats.Experience / _playerStats.ExperienceRequired;
    }

    private void UpdateLevelText(int level)
    {
        _levelText.text = $"Level: {level}";
    }

    private void UpdateHealthBar()
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < _playerStats.CurrentHealth)
            {
                _hearts[i].sprite = _fullHeart;
            }
            else
            {
                _hearts[i].sprite = _emptyHeart;
            }

            if (i < _playerStats.MaxHealth)
            {
                _hearts[i].enabled = true;
            }
            else
            {
                _hearts[i].enabled = false;
            }
        }
    }

    private void UpdateAmmoCount()
    {
        _ammoCountText.text = _weapon.currentAmmo.ToString();
    }

    private void UpdateOrbCount()
    {
        _orbCountTextHUD.text = _playerStats.EnergyOrbs.ToString();
        _orbCountText.text = $"{_playerStats.EnergyOrbs} orbs left";
    }

    public void ShowPermaUpgradesPanel()
    {
        Time.timeScale = 0f;
        _permaUpgradePanel.SetActive(true);
    }
    
    public void HidePermaUpgradesPanel()
    {
        Time.timeScale = 1f;
        SaveGamePlsHuHu?.Invoke();
        _permaUpgradePanel.SetActive(false);
    }
    
    
    private void HideUpgradePanel()
    {
        Time.timeScale = 1f;
        _upgradePanel.SetActive(false);
    }

    private void ShowUpgradePanel()
    {
        Time.timeScale = 0f;
        _upgradePanel.SetActive(true);
    }

    private void ShowGameOverPanel()
    {
        Time.timeScale = 0f;
        StartCoroutine(WaitForSecondsToReload("MainGame"));
    }

    public void ShowPauseMenu()
    {
        Debug.Log("paused");
        Time.timeScale = 0f;
        _pauseMenuPanel.SetActive(true);
        _isPaused = true;
    }
    
    public void HidePauseMenu()
    {
        Debug.Log("resume");
        Time.timeScale = 1f;
        _pauseMenuPanel.SetActive(false);
        _isPaused = false;
    }
    
    public void RestartButton()
    {
        _areYouSureRestartPanel.SetActive(true);
    }
    
    public void QuitButton()
    {
        _areYouSureQuitPanel.SetActive(true);
        SoundManager.Instance.StopPlayingBGM(SoundManager.Sounds.GameBGM);
    }

    public void SureRestartButton()
    {
        Debug.Log("restarted");
        _pauseMenuPanel.SetActive(false);
        KillSelf?.Invoke();
        StartCoroutine(WaitForSecondsToReload("MainGame"));
    }
    
    public void SureQuitButton()
    {
        Debug.Log("quit");
        _pauseMenuPanel.SetActive(false);
        KillSelf?.Invoke();
        StartCoroutine(WaitForSecondsToReload("MainMenu"));
    }

    public void PauseChecker()
    {
        if (_isPaused)
        {
            HidePauseMenu();
        }
        else
        {
            ShowPauseMenu();
        }
    }
    

    IEnumerator WaitForSecondsToReload(string scene)
    {
        
        SoundManager.Instance.PlayOnce(SoundManager.Sounds.PlayerDeath);
        if (_playerStats.EnergyOrbs > 0)
        {
            _lostOrbText.text = $"You lost {_playerStats.EnergyOrbs} orbs. sayang tuloy hays";
            _gameOverPanel.SetActive(true);
        }
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        ObjectPool.Instance.Dispose(ObjectPool.Instance._objectsPool);
        ObjectPool.Instance._objectsPool.Clear();
        SaveGameData();
        ResetWeaponToDefault?.Invoke();
        SceneManager.LoadScene(scene);
        
    }

    public void SaveGameData()
    {
        SaveSystem.Instance.SaveGame();
    }

}
