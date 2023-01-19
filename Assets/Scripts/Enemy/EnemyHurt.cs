using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    
    private bool _isHit;
    private Weapon _weapon;
    [SerializeField] private float _health;
    
    [SerializeField] private GameObject _XPOrb;
    [SerializeField] private GameObject _damageText;
    [SerializeField] private Transform _damageTextPos;

    private void Start()
    {
        _weapon = FindObjectOfType<WeaponController>().Weapon;

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PlayerNormalBullet")  || col.gameObject.tag.Equals("PlayerExplosionRadius")) 
        {
            if(_isHit) return;
            _isHit = true;
            TakeDamage(_weapon.damage);
                
            if(_health <= 0)
            {
                gameObject.SetActive(false);
                DropExperienceOrb();
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
        _health -= damage;
    }

    private void ShowDamage(string damage)
    {
        GameObject text = ObjectPool.Instance.GetObject(_damageText, _damageTextPos.position);
        text.GetComponent<TextMeshPro>().text = damage;
        text.SetActive(true);
    }

    private void DropExperienceOrb()
    {
        GameObject XPOrb = ObjectPool.Instance.GetObject(_XPOrb, transform.position);
        XPOrb.SetActive(true);
    }
}
