using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExploding : MonoBehaviour
{
    [SerializeField] private GameObject _explosionRadius;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Enemy")|| col.gameObject.tag.Equals("EnemyRanged") || col.gameObject.tag.Equals("EnemyLongRanged") || col.gameObject.tag.Equals("EnemyProjectile"))
        {
            GameObject circle = ObjectPool.Instance.GetObject(_explosionRadius, transform.position);
            circle.SetActive(true);
        }
    }
}
