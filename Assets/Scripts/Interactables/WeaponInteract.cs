using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponInteract : MonoBehaviour
{
    public static event Action<GameObject> WeaponCollected;  
    public GameObject[] weapons;
    public static GameObject weapon;
    private int randomIndex;

    private void Awake()
    {
        PlayerInteraction.InteractedWithWeapon += PickUpWeapon;
        randomIndex = Random.Range(0, weapons.Length);
        weapon = weapons[randomIndex];
    }

    private void OnDisable()
    {
        PlayerInteraction.InteractedWithWeapon -= PickUpWeapon;
    }

    private void Start()
    {
        Debug.Log($"Weapon available: {weapon.name}");
        UpdateIcon();
    }

    private void DeactivateWeapon()
    {
        Destroy(gameObject);
        Debug.Log("WeaponSelected");
    }
    
    private void PickUpWeapon()
    {
        ObjectPool.Instance.Dispose(ObjectPool.Instance._objectsPool);
        ObjectPool.Instance._objectsPool.Clear();
        WeaponCollected?.Invoke(weapon);
        DeactivateWeapon();
    }


    private void UpdateIcon()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<WeaponController>().Weapon._icon;
    }
}
