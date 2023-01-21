using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    
    public bool isKnockBack;
    private Weapon _weapon;
    private Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void OnEnable()
    {
        _weapon = WeaponHolder.selectedWeapon;
    }

    public void KnockBack()
    {
        if (isKnockBack) return;
        isKnockBack = true;
        Vector2 knockbackDirection = (transform.position - _player.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * _weapon.knockBackForce, ForceMode2D.Impulse);
        StartCoroutine(StopKnockback(.1f));

    }
    
    IEnumerator StopKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isKnockBack = false;
    }
    
}
