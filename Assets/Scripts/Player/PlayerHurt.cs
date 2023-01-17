using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    
    private bool _isKnockBack = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            KnockBack(col.gameObject.GetComponent<Transform>());
        }
    }

    private void KnockBack(Transform sender)
    {
        if (_isKnockBack) return;
        _isKnockBack = true;
        Vector2 knockbackDirection = (transform.position - sender.position).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * 20, ForceMode2D.Impulse);
        StartCoroutine(StopKnockback(.1f));
    }
    
    IEnumerator StopKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _isKnockBack = false;
    }
}
