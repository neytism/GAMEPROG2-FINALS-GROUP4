using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private void OnEnable()
    {
        StartCoroutine(BulletLife(gameObject));
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        { 
            DeactivateBullet();
        }
    }

    IEnumerator BulletLife(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        DeactivateBullet();
    }

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<TrailRenderer>().Clear();
    }
}
