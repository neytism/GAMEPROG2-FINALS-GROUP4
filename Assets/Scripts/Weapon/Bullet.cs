using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particleObj;
    public int maxPierces;
    public int currentPierces;
    private bool isColliding;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        isColliding = false;
        currentPierces = 1;
        maxPierces = FindObjectOfType<WeaponHolder>().SelectedWeaponType.Weapon.maxPiercing;
        StartCoroutine(BulletLife(gameObject));
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("EnemyRanged") || col.gameObject.tag.Equals("EnemyLongRanged") || col.gameObject.tag.Equals("EnemyProjectile") || col.gameObject.tag.Equals("EnemyBoss"))
        {
            if (!isColliding)
            {
                GameObject particle = ObjectPool.Instance.GetObject(particleObj, transform.position);
                particle.SetActive(true);
                
                isColliding = true;
                if (currentPierces >= maxPierces)
                {
                    DeactivateBullet();
                }

                currentPierces++;
            }
        }
        
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            isColliding = false;
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
