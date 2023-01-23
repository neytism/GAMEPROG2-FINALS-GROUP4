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
    private float _tempHealth;
    private float _currenHealth;
    
    [SerializeField] private GameObject _damageText;
    [SerializeField] private Transform _damageTextPos;
    
    [SerializeField] private GameObject _bloodParticle;

    private void Awake()
    {
        _tempHealth = _health;
    }

    private void OnEnable()
    {
        _currenHealth = _tempHealth;
        _weapon = WeaponHolder.selectedWeapon;
        EnemySpawn.NewEnemyScycle += IncreaseEnemyHealth;
    }

    private void OnDisable()
    {
        EnemySpawn.NewEnemyScycle -= IncreaseEnemyHealth;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PlayerNormalBullet")  || col.gameObject.tag.Equals("PlayerExplosionRadius")) 
        {
            if(_isHit) return;
            _isHit = true;
            
            TakeDamage(_weapon.damage);
            SoundManager.Instance.PlayOnce(SoundManager.Sounds.EnemyHit);
                
            if(_currenHealth <= 0)
            {
                ParticleEmit();
                gameObject.SetActive(false);
                GetComponent<EnemyDrop>().DropOrbChance();
            }
            else
            {
                GetComponent<EnemyKnockBack>().KnockBack();
            }
                
            ShowDamage(_weapon.damage);

            _isHit = false;
        }
    }

   

    private void TakeDamage(float damage)
    {
        _currenHealth -= damage;
    }

    private void ShowDamage(float damage)
    {
        GameObject text = ObjectPool.Instance.GetObject(_damageText, _damageTextPos.position);
        text.GetComponent<TextMeshPro>().text = Math.Round(damage).ToString();
        text.SetActive(true);
    }

    private void ParticleEmit()
    {
        GameObject blood = ObjectPool.Instance.GetObject(_bloodParticle, transform.position);
        blood.SetActive(true);
    }

    private void IncreaseEnemyHealth()
    {
        _tempHealth = _tempHealth * 2;
    }

    
}
