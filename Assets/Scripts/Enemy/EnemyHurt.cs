using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHurt : MonoBehaviour
{
    
    private bool _isHit;
    private Weapon _weapon;
    private Weapon _tempWeapon;
    [SerializeField] private float _health;
    private float _currenHealth;
    
    [SerializeField] private GameObject _damageText;
    [SerializeField] private Transform _damageTextPos;


    private void OnEnable()
    {
        _currenHealth = _health;
        _weapon = WeaponHolder.selectedWeapon;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PlayerNormalBullet")  || col.gameObject.tag.Equals("PlayerExplosionRadius")) 
        {
            if(_isHit) return;
            _isHit = true;
            
            TakeDamage(_weapon.damage);
                
            if(_currenHealth <= 0)
            {
                gameObject.SetActive(false);
                GetComponent<EnemyDrop>().DropOrbChance();
            }
            else
            {
                GetComponent<EnemyKnockBack>().KnockBack();
            }
                
            ShowDamage(_weapon.damage.ToString());

            _isHit = false;
        }
    }

   

    private void TakeDamage(float damage)
    {
        _currenHealth -= damage;
    }

    private void ShowDamage(string damage)
    {
        GameObject text = ObjectPool.Instance.GetObject(_damageText, _damageTextPos.position);
        text.GetComponent<TextMeshPro>().text = damage;
        text.SetActive(true);
    }

    
}
