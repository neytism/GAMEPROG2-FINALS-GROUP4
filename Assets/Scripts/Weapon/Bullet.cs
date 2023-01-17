using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _maxPierces;
    private int _currentPierces;
    private bool isColliding;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        isColliding = false;
        _currentPierces = 1;
        _maxPierces = FindObjectOfType<WeaponHolder>().SelectedWeaponType.Weapon.maxPiercing;
        StartCoroutine(BulletLife(gameObject));
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            if (!isColliding)
            {
                isColliding = true;
                if (_currentPierces >= _maxPierces)
                {
                    DeactivateBullet();
                }
                _currentPierces++;
            }
        }
        
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            isColliding = false;
            Debug.Log($"Pierced {_currentPierces}");
        }
    }

    IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(.5f);
        DeactivateBullet();
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<TrailRenderer>().Clear();
    }
}
