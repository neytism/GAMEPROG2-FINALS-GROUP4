using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
    }


    private void OnEnable()
    {
        StartCoroutine(ProjectileLife());
    }
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            DeactivateProjectile();
        }
    }

    IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(5);
        DeactivateProjectile();
    }

    private void DeactivateProjectile()
    {
        gameObject.SetActive(false);
    }
}
