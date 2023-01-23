using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    public static event Action ReduceHealth;
    private bool _isKnockBack = false;
    private bool _isInvincible = false;
    private Rigidbody2D _rb;
    private Collider2D _col;

    public bool IsKnockBack => _isKnockBack;
    public bool IsInvincible => _isInvincible;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("EnemyRanged") || col.gameObject.tag.Equals("EnemyLongRanged") || col.gameObject.tag.Equals("EnemyProjectile") || col.gameObject.tag.Equals("EnemyBullet")) && !_isInvincible)
        {
            ReduceHealth?.Invoke();
            SoundManager.Instance.PlayOnce(SoundManager.Sounds.PlayerHurt);
            KnockBack(col.gameObject.GetComponent<Transform>());
        }
    }

    private void KnockBack(Transform sender)
    {
        if (_isKnockBack) return;
        if (_isInvincible) return;
        //will not knock-back if player is already on knock-back state or is invincible
        
        _isKnockBack = true;
        _isInvincible = true;
        
        //can add animation for playerHurt
        
        //knockback
        Vector2 knockbackDirection = (transform.position - sender.position).normalized;
        _rb.AddForce(knockbackDirection * 20, ForceMode2D.Impulse);
        StartCoroutine(Invincible(2f));
        StartCoroutine(StopKnockback(.075f));
    }
    
    IEnumerator StopKnockback(float duration)
    {
        //knockback duration
        yield return new WaitForSeconds(duration);
        _rb.velocity = Vector2.zero;
        _isKnockBack = false;
    }

    private IEnumerator Invincible(float duration)
    {
        //invincible after knock-back for a short time
        yield return new WaitForSeconds(duration);
        _isInvincible = false;
    }
}
