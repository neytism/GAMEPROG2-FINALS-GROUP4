using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public float speed;
    public float stoppingRange;
    public float retreatRange;

    private float btwShotInterval;
    public float startBtwShotInterval;

    public GameObject projectile;
    private Transform _target;
    private EnemyKnockBack _kb;

    void Start()
    {
        _target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        _kb = GetComponent<EnemyKnockBack>();
        btwShotInterval = startBtwShotInterval;
    }

    void Update()
    {
        Chase();
        SummonProjectile();
    }
    
    public void Chase() //follow player 
    {
        Vector2 direction = _target.position - transform.position;
        direction.Normalize();
        
        if (!_kb.isKnockBack)
        {
            
            if(Vector2.Distance(transform.position, _target.position) > stoppingRange)
            {
                // If enemy is far away, he moves closer.
                transform.position = Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
            }else if(Vector2.Distance(transform.position, _target.position) < stoppingRange && Vector2.Distance(transform.position, _target.position) > retreatRange)
            {
                // If enemy is close enough to the player, enemy stops.
                transform.position = this.transform.position;
            }else if (Vector2.Distance(transform.position, _target.position) < retreatRange)
            {
                // If player gets closen the gap of the retreat range, the enemy move farther away.
                transform.position = Vector2.MoveTowards(transform.position, _target.position, -speed * Time.deltaTime);
            }
        }
        
    }

    private void SummonProjectile()
    {
        if(btwShotInterval <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            btwShotInterval = startBtwShotInterval;
        } else
        {
            btwShotInterval -= Time.deltaTime;
        }
    }

}
