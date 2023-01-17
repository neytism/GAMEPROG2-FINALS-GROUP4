using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private PlayerMovement _player;
    private Weapon _weapon;
    private float _health;

    [SerializeField] private float _speed;
    private float _KBDuration;
    private bool _isKnockBack;
    private bool _isHit;

    [SerializeField] private GameObject _XPOrb;
    [SerializeField] private GameObject _damageText;
    [SerializeField] private Transform _damageTextPos;
    

        private void OnEnable()
    {
        _health = 20;
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        _weapon = FindObjectOfType<WeaponController>().Weapon;

    }
    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    public void Chase() //follow player while facing target
    {
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();

        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
            if (col.gameObject.tag.Equals("PlayerBullet")) 
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
                    KnockBack(_player.transform);
                }
                
                ShowDamage(_weapon.damage.ToString());
                Debug.Log("enemy hit");

                _isHit = false;
            }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy") ||col.gameObject.tag.Equals("Player")) 
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Chase();
        }
    }

    private void KnockBack(Transform sender)
    {
        if (_isKnockBack) return;
        _isKnockBack = true;
        Vector2 knockbackDirection = (transform.position - sender.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * _weapon.knockBackForce, ForceMode2D.Impulse);
        StartCoroutine(StopKnockback(.1f));

    }
    
    IEnumerator StopKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _isKnockBack = false;
        Chase();
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
